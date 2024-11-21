using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("DimCustomers")]
    public class DimCustomers
    {
        [Key]
        public int CustomerKey { get; set; }

        [Required, MaxLength(5)]
        public string CustomerID { get; set; }

        [Required, MaxLength(40)]
        public string CompanyName { get; set; }

        [MaxLength(30)]
        public string ContactName { get; set; }

        [MaxLength(30)]
        public string ContactTitle { get; set; }

        [MaxLength(15)]
        public string City { get; set; }

        [MaxLength(15)]
        public string Country { get; set; }
    }

}
