using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    [Table("FactClienteAtendido", Schema = "dbo")]
    public class FactClienteAtendido
    {
        [Key]
        public int ClienteAtendidoKey { get; set; }

        public int EmployeeKey { get; set; }

        public int NumeroDeClientes { get; set; }
    }
}
