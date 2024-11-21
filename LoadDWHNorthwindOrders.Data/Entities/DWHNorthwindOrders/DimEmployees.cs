
namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    public class DimEmployees
    {
        public int EmployeeID { get; set; } 
        public string LastName { get; set; } 
        public string FirstName { get; set; } 
        public string Title { get; set; } 
        public string TitleOfCourtesy { get; set; } 
        public DateTime? BirthDate { get; set; } 
        public DateTime? HireDate { get; set; } 
        public string TerritoryID { get; set; }
    }
}
