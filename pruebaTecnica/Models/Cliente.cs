using System;
using System.Collections.Generic;

namespace pruebaTecnica.Models;

public partial class Cliente
{
    public int IdClientes { get; set; }

    public string? TipoIentificacion { get; set; }

    public int? NumeroIdentificacion { get; set; }

    public string? Nombres { get; set; }

    public string? Apellido { get; set; }

    public string? Corre { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();

    public virtual ICollection<Transaccione> Transacciones { get; } = new List<Transaccione>();
}
