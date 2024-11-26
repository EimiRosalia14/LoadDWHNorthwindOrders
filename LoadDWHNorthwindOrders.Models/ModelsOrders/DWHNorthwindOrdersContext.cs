﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoadDWHNorthwindOrders.Models.DwhNorthwindOrders.ModelsOrders;

public partial class DWHNorthwindOrdersContext : DbContext
{
    public DWHNorthwindOrdersContext(DbContextOptions<DWHNorthwindOrdersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FactClienteAtendido> FactClienteAtendidos { get; set; }

    public virtual DbSet<FactOrder> FactOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FactClienteAtendido>(entity =>
        {
            entity.HasKey(e => e.ClienteAtendidoKey).HasName("PK__FactClie__B70C6B931CEA20DE");

            entity.ToTable("FactClienteAtendido");

            entity.HasIndex(e => e.EmployeeKey, "IX_FactClienteAtendido_EmployeeKey");
        });

        modelBuilder.Entity<FactOrder>(entity =>
        {
            entity.HasKey(e => e.OrderKey).HasName("PK__FactOrde__E6D597D0BE1DA372");

            entity.HasIndex(e => new { e.Año, e.Mes, e.CantidadProductos }, "IX_FactOrders_YearMonthCantidad");

            entity.HasIndex(e => e.OrderId, "UQ__FactOrde__C3905BAE219D1D8D").IsUnique();

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.TotalVenta).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}