using LoadDWHNorthwindOrders.Data.Entities.Northwind;
using Microsoft.EntityFrameworkCore;

namespace LoadDWHNorthwindOrders.Data.Context
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Shippers> Shippers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla Categories
            modelBuilder.Entity<Categories>()
                .HasKey(c => c.CategoryID);

            modelBuilder.Entity<Categories>()
                .Ignore(c => c.Picture); // Ignorar la columna Picture si no se usa

            // Configuración de la tabla Customers
            modelBuilder.Entity<Customers>()
                .Property(c => c.CustomerID)
                .HasMaxLength(5)
                .IsFixedLength();

            modelBuilder.Entity<Customers>()
                .Property(c => c.CompanyName)
                .HasMaxLength(40)
                .IsRequired();

            // Configuración de la tabla Products
            modelBuilder.Entity<Products>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
