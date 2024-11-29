using LoadDWHNorthwindOrders.Data.Entities.DWHNorthwindOrders;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<FactClienteAtendido> FactClienteAtendidos { get; set; }
        public DbSet<FactOrder> FactOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de DimCustomers
            modelBuilder.Entity<DimCustomers>()
                .Property(c => c.CustomerID)
                .HasMaxLength(5)
                .IsFixedLength();

            modelBuilder.Entity<DimCustomers>()
                .Property(c => c.CompanyName)
                .HasMaxLength(40)
                .IsRequired();

            // Configuración de DimProducts
            modelBuilder.Entity<DimProducts>()
                .Property(p => p.ProductName)
                .HasMaxLength(40)
                .IsRequired();

            // Configuración de DimEmployees
            modelBuilder.Entity<DimEmployees>()
                .Property(e => e.LastName)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<DimEmployees>()
                .Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsRequired();

            // Configuración de DimShippers
            modelBuilder.Entity<DimShippers>()
                .Property(s => s.CompanyName)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<DimShippers>()
                .Property(s => s.Phone)
                .HasMaxLength(24);

            // Configuración de DimCategories
            modelBuilder.Entity<DimCategories>()
                .Property(c => c.CategoryName)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<FactOrder>()
                .Property(f => f.Country)
                .HasMaxLength(15)
                .HasColumnName("Country"); 


            base.OnModelCreating(modelBuilder);
        }
    }
}
