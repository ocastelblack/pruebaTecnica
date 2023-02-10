using System;
using System.Collections.Generic;

namespace pruebaTecnica.Models;

public partial class Transaccione
{
    public int IdTransacciones { get; set; }

    public string? TipoTransaccion { get; set; }

    public int? montoTransaccion { get; set; }

    public string? DescripcionTransaccion { get; set; }

    public int? IdCliente { get; set; }

    public int? IdProducto { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
