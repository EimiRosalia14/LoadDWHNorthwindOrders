using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("FactOrders", Schema = "dbo")]
    public class FactOrder
    {
        [Key]
        public int OrderKey { get; set; }

        public int OrderId { get; set; }

        public int CustomerKey { get; set; }

        public int EmployeeKey { get; set; }

        public int? ShipVia { get; set; }

        public int Año { get; set; }

        public int Mes { get; set; }

        public decimal? TotalVenta { get; set; }

        public int CantidadProductos { get; set; }
    }
}
