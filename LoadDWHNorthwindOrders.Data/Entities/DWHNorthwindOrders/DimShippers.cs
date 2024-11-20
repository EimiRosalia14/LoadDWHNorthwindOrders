using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    public class DimShippers
    {
        public int ShipperID { get; set; }
        public string CompanyName { get; set; } 
        public string Phone { get; set; }
    }
}
