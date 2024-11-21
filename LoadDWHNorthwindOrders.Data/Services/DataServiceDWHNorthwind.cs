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
                await LoadDimCustomersAsync();
                await LoadDimEmployeesAsync();
                await LoadDimShippersAsync();
                await LoadDimCategoriesAsync();
                await LoadDimProductsAsync();

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

        private async Task LoadDimCustomersAsync()
        {
            try
            {
                // Limpiar tabla DimCustomers con procedimiento almacenado
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimCustomers");

                // Cargar datos desde Northwind
                var customers = await _northwindContext.Customers
                    .AsNoTracking()
                    .Select(c => new DimCustomers
                    {
                        CustomerID = c.CustomerID,
                        CompanyName = c.CompanyName,
                        ContactName = c.ContactName,
                        ContactTitle = c.ContactTitle,
                        Address = c.Address,
                        City = c.City,
                        Region = c.Region,
                        PostalCode = c.PostalCode,
                        Country = c.Country
                    }).ToListAsync();

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
                // Limpiar tabla DimEmployees con procedimiento almacenado
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimEmployees");

                // Cargar datos desde Northwind
                var employees = await _northwindContext.Employees
                    .AsNoTracking()
                    .Select(e => new DimEmployees
                    {
                        EmployeeID = e.EmployeeID,
                        LastName = e.LastName,
                        FirstName = e.FirstName,
                        Title = e.Title,
                        TitleOfCourtesy = e.TitleOfCourtesy,
                        BirthDate = e.BirthDate,
                        HireDate = e.HireDate,
                        TerritoryID = null
                    }).ToListAsync();

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
                // Limpiar tabla DimShippers con procedimiento almacenado
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimShippers");

                // Cargar datos desde Northwind
                var shippers = await _northwindContext.Shippers
                    .AsNoTracking()
                    .Select(s => new DimShippers
                    {
                        ShipperID = s.ShipperID,
                        CompanyName = s.CompanyName,
                        Phone = s.Phone
                    }).ToListAsync();

                await _dwhContext.DimShippers.AddRangeAsync(shippers);
                await _dwhContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la dimensión de transportistas: " + ex.Message);
            }
        }

        private async Task LoadDimCategoriesAsync()
        {
            try
            {
                // Limpiar tabla DimCategories con procedimiento almacenado
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimCategories");

                // Cargar datos desde Northwind
                var categories = await _northwindContext.Categories
                    .AsNoTracking()
                    .Select(c => new DimCategories
                    {
                        CategoryID = c.CategoryID,
                        CategoryName = c.CategoryName,
                        Description = c.Description
                    }).ToListAsync();

                await _dwhContext.DimCategories.AddRangeAsync(categories);
                await _dwhContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la dimensión de categorías: " + ex.Message);
            }
        }

        private async Task LoadDimProductsAsync()
        {
            try
            {
                // Limpiar tabla DimProducts con procedimiento almacenado
                await _dwhContext.Database.ExecuteSqlRawAsync("EXEC sp_ClearDimProducts");

                // Cargar datos desde Northwind
                var products = await _northwindContext.Products
                    .AsNoTracking()
                    .Select(p => new DimProducts
                    {
                        ProductID = p.ProductID,
                        ProductName = p.ProductName,
                        SupplierID = p.SupplierID,
                        CategoryID = p.CategoryID,
                        QuantityPerUnit = p.QuantityPerUnit,
                        UnitPrice = p.UnitPrice
                    }).ToListAsync();

                await _dwhContext.DimProducts.AddRangeAsync(products);
                await _dwhContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la dimensión de productos: " + ex.Message);
            }
        }
    }
}
