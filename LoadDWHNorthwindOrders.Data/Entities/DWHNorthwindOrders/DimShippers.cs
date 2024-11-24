
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("DimShippers")]
    public class DimShippers
    {
        [Key]
        public int ShipperKey { get; set; }

        [Required]
        public int ShipperID { get; set; }

        [Required, MaxLength(40)]
        public string? CompanyName { get; set; }

        [MaxLength(24)]
        public string? Phone { get; set; }
    }

}
