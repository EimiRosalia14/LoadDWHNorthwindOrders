
using System.ComponentModel.DataAnnotations;

namespace LoadDWHNorthwindOrders.Data.Entities.Northwind
{
    public class Categories
    {
        [Key] // Define que esta es la clave primaria
        public int CategoryID { get; set; }

        [MaxLength(15)] // Alineado con la base de datos Northwind
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }
    }
}
