
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("DimProducts")]
    public class DimProducts
    {
        [Key]
        public int ProductKey { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required, MaxLength(40)]
        public string? ProductName { get; set; }

        public int? CategoryID { get; set; } 
    }

}
