using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pruebaTecnica.Models;

public partial class PruebaContext : DbContext
{
    public PruebaContext()
    {
    }

    public PruebaContext(DbContextOptions<PruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdClientes).HasName("PK__clientes__470BDBA0F7E85B54");

            entity.ToTable("clientes");

            entity.Property(e => e.IdClientes).HasColumnName("idClientes");
            entity.Property(e => e.Apellido)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Corre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("corre");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("datetime")
                .HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombres)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.NumeroIdentificacion).HasColumnName("numeroIdentificacion");
            entity.Property(e => e.TipoIentificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProductos).HasName("PK__producto__A26E462D32085725");

            entity.ToTable("productos");

            entity.Property(e => e.IdProductos).HasColumnName("idProductos");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.ExenteGmf)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("exenteGmf");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
            entity.Property(e => e.NumeroCuenta).HasColumnName("numeroCuenta");
            entity.Property(e => e.Saldo).HasColumnName("saldo");
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipoCuenta");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__productos__id_Cl__398D8EEE");
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.IdTransacciones).HasName("PK__transacc__B0F4E0DEC297F51C");

            entity.ToTable("transacciones");

            entity.Property(e => e.IdTransacciones).HasColumnName("idTransacciones");
            entity.Property(e => e.DescripcionTransaccion)
                .IsUnicode(false)
                .HasColumnName("descripcionTransaccion");
            entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
            entity.Property(e => e.IdProducto).HasColumnName("id_Producto");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipoTransaccion");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__transacci__id_Cl__3C69FB99");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__transacci__id_Pr__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
