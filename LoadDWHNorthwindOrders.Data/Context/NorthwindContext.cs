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
        public DbSet<VwClienteAtendido> VwClienteAtendidos { get; set; }
        public DbSet<VwOrder> VwOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla Categories
            modelBuilder.Entity<Categories>()
                .HasKey(c => c.CategoryID);

            modelBuilder.Entity<Categories>()
                .Ignore(c => c.Picture); // Ignorar la columna Picture

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

            modelBuilder.Entity<VwClienteAtendido>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VwClienteAtendido", "DWH");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(31);
            });

            modelBuilder.Entity<VwOrder>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VwOrders", "DWH");

                entity.Property(e => e.ClienteId)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .HasColumnName("ClienteID");
                entity.Property(e => e.ClienteNombre)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
                entity.Property(e => e.EmpleadoNombre)
                    .IsRequired()
                    .HasMaxLength(31);
                entity.Property(e => e.OrdenId).HasColumnName("OrdenID");
                entity.Property(e => e.TransportistaId).HasColumnName("TransportistaID");
                entity.Property(e => e.TransportistaNombre)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
