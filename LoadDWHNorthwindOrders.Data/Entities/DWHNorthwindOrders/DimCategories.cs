using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders
{
    public class DimCategories
    {
        public int CategoryID { get; set; } 
        public string CategoryName { get; set; } 
        public string Description { get; set; }
    }
}
