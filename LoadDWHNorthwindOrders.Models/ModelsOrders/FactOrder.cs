﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace LoadDWHNorthwindOrders.Models.DWHNorthwindOrders.ModelsOrders;

public partial class FactOrder
{
    public int OrderKey { get; set; }

    public int OrderId { get; set; }

    public int CustomerKey { get; set; }

    public int EmployeeKey { get; set; }

    public string Country { get; set; }

    public int? ShipVia { get; set; }

    public int Año { get; set; }

    public int Mes { get; set; }

    public decimal? TotalVenta { get; set; }

    public int CantidadProductos { get; set; }
}