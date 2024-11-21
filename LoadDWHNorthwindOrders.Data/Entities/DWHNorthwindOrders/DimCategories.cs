using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("DimCategories")]
    public class DimCategories
    {
        [Key]
        public int CategoryKey { get; set; } 
        public int CategoryID { get; set; } 
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
