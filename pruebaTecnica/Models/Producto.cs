using System;
using System.Collections.Generic;

namespace pruebaTecnica.Models;

public partial class Producto
{
    public int IdProductos { get; set; }

    public string? TipoCuenta { get; set; }

    public string? NumeroCuenta { get; set; }

    public string? Estado { get; set; }

    public int? Saldo { get; set; }

    public string? ExenteGmf { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; } = new List<Transaccione>();
}
