using LoadDWHNorthwindOrders.Data.Context;
using LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders;
using LoadDWHNorthwindOrders.Data.Interfaces;
using LoadDWHNorthwindOrders.Data.Result;
using Microsoft.EntityFrameworkCore;

namespace LoadDWHNorthwindOrders.Data.Services
{
    public class DataServiceDWHNorthwind : IDataServiceDWHNorthwind
    {
        private readonly NorthwindContext _northwindContext;
        private readonly DWHNorthwindOrdersContext _dwhContext;

        public DataServiceDWHNorthwind(NorthwindContext northwindContext, DWHNorthwindOrdersContext dwhContext)
        {
            _northwindContext = northwindContext;
            _dwhContext = dwhContext;
        }
    
        public async Task<OperationResult> LoadDWHAsync()
        {
            var result = new OperationResult();

            try
            {
                await _dwhContext.FactOrders.ExecuteDeleteAsync();
                await _dwhContext.FactClienteAtendidos.ExecuteDeleteAsync();

                await LoadDimCategoriesAsync();
                await LoadDimProductsAsync();
                await LoadDimCustomersAsync();
                await LoadDimEmployeesAsync();
                await LoadDimShippersAsync();
                await LoadFactOrders();
                await LoadFactClienteAtendido();

                result.Success = true;
                result.Message = "Carga de Data Warehouse completada exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al cargar el Data Warehouse: {ex.Message}";
            }

            return result;
        }


        private async Task LoadDimCategoriesAsync()
        {
            try
            {
                // Limpiamos DimProducts (tabla dependiente) primero
                await _dwhContext.Database.ExecuteSqlRawAsync("ALTER TABLE DimProducts NOCHECK CONSTRAINT ALL");
                await _dwhContext.DimProducts.ExecuteDeleteAsync();
                await _dwhContext.Database.ExecuteSqlRawAsync("ALTER TABLE DimProducts CHECK CONSTRAINT ALL");

                // Limpiamos DimCategories
                await _dwhContext.DimCategories.ExecuteDeleteAsync();

                // Cargamos datos de DimCategories
                var categories = await _northwindContext.Categories
                    .AsNoTracking()
                    .Select(c => new DimCategories
                    {
                        CategoryID = c.CategoryID,
                        CategoryName = c.CategoryName ?? "Sin nombre",
                        Description = c.Description ?? "Sin descripción"
                    })
                    .ToListAsync();

                await _dwhContext.DimCategories.AddRangeAsync(categories);
                await _dwhContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar DimCategories: " + ex.Message);
            }
        }

        private async Task LoadDimProductsAsync()
        {
            try
            {
                // Asegurarnos de que DimCategories ya esté cargada antes de DimProducts
                if (!await _dwhContext.DimCategories.AnyAsync())
                {
                    throw new Exception("No se pueden cargar productos sin categorías. Ejecute primero LoadDimCategoriesAsync.");
                }

                // Limpiamos DimProducts
                await _dwhContext.DimProducts.ExecuteDeleteAsync();

                // Cargamos datos de DimProducts
                var products = await _northwindContext.Products
                    .AsNoTracking()
                    .Select(p => new DimProducts
                    {
                        ProductID = p.ProductID,
                        ProductName = p.ProductName ?? "Sin nombre",
                        CategoryID = p.CategoryID
                    })
                    .ToListAsync();

                await _dwhContext.DimProducts.AddRangeAsync(products);
                await _dwhContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar DimProducts: " + ex.Message);
            }
        }

        private async Task LoadDimCustomersAsync()
        {
            try
            {
                await _dwhContext.DimCustomers.ExecuteDeleteAsync();

                var customers = await _northwindContext.Customers
                    .AsNoTracking()
                    .Select(c => new DimCustomers
                    {
                        CustomerID = c.CustomerID.Trim(),
                        CompanyName = c.CompanyName ?? "Sin nombre",
                        ContactName = c.ContactName ?? "No disponible",
                        ContactTitle = c.ContactTitle ?? "No disponible",
                        City = c.City ?? "No especificada",
                        Country = c.Country ?? "Desconocido"
                    })
                    .ToListAsync();

                await _dwhContext.DimCustomers.AddRangeAsync(customers);
                await _dwhContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar DimCustomers: " + ex.Message);
            }
        }

        private async Task LoadDimEmployeesAsync()
        {
            try
            {
                await _dwhContext.DimEmployees.ExecuteDeleteAsync();

                var employees = await _northwindContext.Employees
                    .AsNoTracking()
                    .Select(e => new DimEmployees
                    {
                        EmployeeID = e.EmployeeID,
                        LastName = e.LastName ?? "Desconocido",
                        FirstName = e.FirstName ?? "Desconocido",
                        Title = e.Title ?? "No disponible",
                        TitleOfCourtesy = e.TitleOfCourtesy ?? "No disponible"
                    })
                    .ToListAsync();

                await _dwhContext.DimEmployees.AddRangeAsync(employees);
                await _dwhContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar DimEmployees: " + ex.Message);
            }
        }

        private async Task LoadDimShippersAsync()
        {
            try
            {
                await _dwhContext.DimShippers.ExecuteDeleteAsync();

                var shippers = await _northwindContext.Shippers
                    .AsNoTracking()
                    .Select(s => new DimShippers
                    {
                        ShipperID = s.ShipperID,
                        CompanyName = s.CompanyName ?? "Sin nombre",
                        Phone = s.Phone ?? "No disponible"
                    })
                    .ToListAsync();

                await _dwhContext.DimShippers.AddRangeAsync(shippers);
                await _dwhContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar DimShippers: " + ex.Message);
            }
        }

        private async Task LoadFactOrders()
        {
            try
            {
                var orders = await _northwindContext.VwOrders.AsNoTracking().ToListAsync();

                var existingOrderIds = await _dwhContext.FactOrders.Select(f => f.OrderId).ToArrayAsync();
                if (existingOrderIds.Any())
                {
                    await _dwhContext.FactOrders.Where(f => existingOrderIds.Contains(f.OrderId))
                                                .ExecuteDeleteAsync();
                }

                var customers = await _dwhContext.DimCustomers
                                                 .AsNoTracking()
                                                 .ToDictionaryAsync(c => c.CustomerID, c => c.CustomerKey);

                var employees = await _dwhContext.DimEmployees
                                                 .AsNoTracking()
                                                 .ToDictionaryAsync(e => e.EmployeeID, e => e.EmployeeKey);

                var shippers = await _dwhContext.DimShippers
                                                .AsNoTracking()
                                                .ToDictionaryAsync(s => s.ShipperID, s => s.ShipperKey);

                var factOrders = new List<FactOrder>();

                foreach (var order in orders)
                {
                    var customerKey = customers.TryGetValue(order.ClienteId ?? string.Empty, out var cKey) ? cKey : 0;
                    var employeeKey = employees.TryGetValue(order.EmpleadoId, out var eKey) ? eKey : 0;
                    var shipperKey = shippers.TryGetValue(order.TransportistaId, out var sKey) ? sKey : 0;

                    FactOrder factOrder = new FactOrder
                    {
                        OrderId = order.OrdenId,
                        CustomerKey = customerKey,
                        EmployeeKey = employeeKey,
                        ShipVia = shipperKey,
                        Año = order.Año ?? 0,
                        Mes = order.Mes ?? 0,
                        TotalVenta = (decimal?)order.TotalVenta ?? 0, 
                        CantidadProductos = order.CantidadProductos ?? 0
                    };

                    factOrders.Add(factOrder);
                }

                if (factOrders.Any())
                {
                    await _dwhContext.FactOrders.AddRangeAsync(factOrders);
                    await _dwhContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar el FactOrders: " + ex.Message);
            }
        }

        private async Task LoadFactClienteAtendido()
        {
            try
            {
                var clienteAtendidos = await _northwindContext.VwClienteAtendidos.AsNoTracking().ToListAsync();

                await _dwhContext.FactClienteAtendidos.ExecuteDeleteAsync();

                var employeeKeys = await _dwhContext.DimEmployees
                                                    .AsNoTracking()
                                                    .ToDictionaryAsync(e => e.EmployeeID, e => e.EmployeeKey);

                var factClientes = clienteAtendidos.Select(customer =>
                {
                    var employeeKey = employeeKeys.ContainsKey(customer.EmployeeId) ? employeeKeys[customer.EmployeeId] : 0;

                    return new FactClienteAtendido
                    {
                        EmployeeKey = employeeKey,
                        NumeroDeClientes = customer.NumeroDeClientes ?? 0
                    };
                }).ToList();

                await _dwhContext.FactClienteAtendidos.AddRangeAsync(factClientes);

                await _dwhContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar el FactClienteAtendido: {ex.Message}");
            }
        }
    }
}
