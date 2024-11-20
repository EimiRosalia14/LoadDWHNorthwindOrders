using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    public class DimProducts
    {
        public int ProductID { get; set; } 
        public string ProductName { get; set; } 
        public int? SupplierID { get; set; } 
        public int? CategoryID { get; set; } 
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
