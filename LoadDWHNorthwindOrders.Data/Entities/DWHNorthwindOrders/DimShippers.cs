
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("DimShippers")]
    public class DimShippers
    {
        [Key]
        public int ShipperKey { get; set; } 
        public int ShipperID { get; set; } 
        public string? CompanyName { get; set; }
        public string? Phone { get; set; }
    }
}
