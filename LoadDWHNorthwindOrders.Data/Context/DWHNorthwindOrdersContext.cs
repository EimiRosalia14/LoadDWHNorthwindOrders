using LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDWHNorthwindOrders.Data.Context
{
    public class DWHNorthwindOrdersContext : DbContext
    {
        public DWHNorthwindOrdersContext(DbContextOptions<DWHNorthwindOrdersContext> options) : base(options)
        {
        }

        public DbSet<DimCategories> DimCategories { get; set; }
        public DbSet<DimCustomers> DimCustomers { get; set; }
        public DbSet<DimEmployees> DimEmployees { get; set; }
        public DbSet<DimProducts> DimProducts { get; set; }
        public DbSet<DimShippers> DimShippers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
