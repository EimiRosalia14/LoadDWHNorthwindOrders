using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("DimCategories")]
    public class DimCategories
    {
        [Key]
        public int CategoryKey { get; set; } // Clave primaria generada automáticamente

        [Required]
        public int CategoryID { get; set; } // ID único alineado con Northwind

        [Required, MaxLength(40)]
        public string CategoryName { get; set; } // Nombre de la categoría, obligatorio

        public string Description { get; set; } // Descripción opcional (puede ser nulo)
    }

}
