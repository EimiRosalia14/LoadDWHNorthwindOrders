using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDWHNorthwindOrders.Data.Entities.Northwind
{
    public class Categories
    {
        public int CategoryID { get; set; } 
        public string CategoryName { get; set; } 
        public string Description { get; set; } 
        public byte[] Picture { get; set; }
    }
}
