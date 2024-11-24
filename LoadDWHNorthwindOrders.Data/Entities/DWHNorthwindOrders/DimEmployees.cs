
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("DimEmployees")]
    public class DimEmployees
    {
        [Key]
        public int EmployeeKey { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required, MaxLength(20)]
        public string? LastName { get; set; }

        [Required, MaxLength(10)]
        public string? FirstName { get; set; }

        [MaxLength(30)]
        public string? Title { get; set; }

        [MaxLength(25)]
        public string? TitleOfCourtesy { get; set; }
    }
}
