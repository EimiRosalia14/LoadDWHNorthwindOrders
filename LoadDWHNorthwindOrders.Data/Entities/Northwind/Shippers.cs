
using System.ComponentModel.DataAnnotations;

namespace LoadDWHNorthwindOrders.Data.Entities.Northwind
{
    public class Shippers
    {
        [Key] 
        public int ShipperID { get; set; }

        [MaxLength(40)]
        public string? CompanyName { get; set; }

        [MaxLength(24)]
        public string? Phone { get; set; }
    }
}
