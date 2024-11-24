using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("DimCategories")]
    public class DimCategories
    {
        [Key]
        public int CategoryKey { get; set; }

        [Required]
        public int CategoryID { get; set; } 

        [Required, MaxLength(40)]
        public string? CategoryName { get; set; } 

        public string? Description { get; set; } 
    }

}
