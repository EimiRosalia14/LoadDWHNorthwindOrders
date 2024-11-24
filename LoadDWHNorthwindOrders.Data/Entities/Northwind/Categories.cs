
using System.ComponentModel.DataAnnotations;

namespace LoadDWHNorthwindOrders.Data.Entities.Northwind
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }

        [MaxLength(15)] 
        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public byte[]? Picture { get; set; }
    }
}
