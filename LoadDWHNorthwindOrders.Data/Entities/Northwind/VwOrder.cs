
namespace LoadDWHNorthwindOrders.Data.Entities.Northwind
{
    public class VwOrder
    {
        public int OrdenId { get; set; }

        public string? ClienteId { get; set; }

        public string? ClienteNombre { get; set; }

        public int EmpleadoId { get; set; }

        public string? EmpleadoNombre { get; set; }

        public int TransportistaId { get; set; }

        public string? TransportistaNombre { get; set; }

        public int? Año { get; set; }

        public int? Mes { get; set; }

        public double? TotalVenta { get; set; }

        public int? CantidadProductos { get; set; }
    }
}
