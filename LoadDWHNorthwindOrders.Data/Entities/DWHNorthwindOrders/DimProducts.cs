
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("DimProducts")]
    public class DimProducts
    {
        [Key]
        public int ProductKey { get; set; }  
        public int ProductID { get; set; } 
        public string? ProductName { get; set; }
        public int? SupplierKey { get; set; }  
        public int? CategoryKey { get; set; } 
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
