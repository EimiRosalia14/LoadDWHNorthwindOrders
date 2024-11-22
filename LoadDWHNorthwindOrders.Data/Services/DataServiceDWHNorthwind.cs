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
                await LoadDimProductsAsync();
                await LoadDimCategoriesAsync();
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


        private async Task LoadDimProductsAsync()
        {
            try
            {
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimProducts");

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
                throw new Exception("Error al cargar la dimensión de productos: " + ex.Message);
            }
        }

        private async Task LoadDimCategoriesAsync()
        {
            try
            {
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimCategories");

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
                throw new Exception("Error al cargar la dimensión de categorías: " + ex.Message);
            }
        }

        private async Task LoadDimCustomersAsync()
        {
            try
            {
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimCustomers");

                var customers = await _northwindContext.Customers
                    .AsNoTracking()
                    .Select(c => new DimCustomers
                    {
                        CustomerID = (c.CustomerID ?? string.Empty).Trim(),
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
                throw new Exception("Error al cargar la dimensión de clientes: " + ex.Message);
            }
        }

        private async Task LoadDimEmployeesAsync()
        {
            try
            {
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimEmployees");

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
                throw new Exception("Error al cargar la dimensión de empleados: " + ex.Message);
            }
        }

        private async Task LoadDimShippersAsync()
        {
            try
            {
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimShippers");

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
                throw new Exception("Error al cargar la dimensión de transportistas: " + ex.Message);
            }
        }


        private async Task LoadFactOrders()
        {
            OperationResult result = new OperationResult();

            try
            {
                var orders = await _northwindContext.VwOrders.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ("Error al cargar el fact de orders: " + ex.Message);
            }
        }

        private async Task LoadFactClienteAtendido()
        {
            OperationResult result = new OperationResult();

            try
            {
                var clienteAtendido = await _northwindContext.VwClienteAtendidos.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ("Error al cargar el fact de clientes atendidos: " + ex.Message);
            }
        }
    }
}
