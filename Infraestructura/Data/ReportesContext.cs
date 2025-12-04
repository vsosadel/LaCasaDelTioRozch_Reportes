using Infraestructura.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infraestructura.Data;

public partial class ReportesContext : DbContext
{
    public ReportesContext()
    {
    }

    public ReportesContext(DbContextOptions<ReportesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdicionalesProducto> AdicionalesProductos { get; set; }

    public virtual DbSet<CanalVenta> CanalVenta { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Detalle> Detalles { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Fotosproducto> FotosProductos { get; set; }

    public virtual DbSet<Orden> Ordenes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pago1> Pagos1 { get; set; }

    public virtual DbSet<Perfil> Perfiles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoCanalVenta> ProductoCanalVenta { get; set; }

    public virtual DbSet<Promocion> Promociones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgres_fdw");

        modelBuilder.Entity<AdicionalesProducto>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("adicionalesproducto", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Preciocosto).HasColumnName("preciocosto");
            entity.Property(e => e.Precioventa).HasColumnName("precioventa");
        });

        modelBuilder.Entity<CanalVenta>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("canalventa", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Descuento).HasColumnName("descuento");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("categoria", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Cocina).HasColumnName("cocina");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Detalle>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("detalle", "fdw_restaurante");

            entity.Property(e => e.Adicionales).HasColumnName("adicionales");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Comanda).HasColumnName("comanda");
            entity.Property(e => e.Entregado)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("entregado");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Iddetalleorigen).HasColumnName("iddetalleorigen");
            entity.Property(e => e.Idorden).HasColumnName("idorden");
            entity.Property(e => e.Observaciones)
                .HasColumnType("character varying")
                .HasColumnName("observaciones");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Producto).HasColumnName("producto");
            entity.Property(e => e.Promocion).HasColumnName("promocion");
            entity.Property(e => e.Solicitado)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("solicitado");
            entity.Property(e => e.Total).HasColumnName("total");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("empresa", "fdw_catalogos");

            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Saludo)
                .HasMaxLength(100)
                .HasColumnName("saludo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Fotosproducto>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("fotosproducto", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Foto)
                .HasColumnType("character varying")
                .HasColumnName("foto");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Registro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registro");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("orden", "fdw_restaurante");

            entity.Property(e => e.Apertura).HasColumnName("apertura");
            entity.Property(e => e.Cajon).HasColumnName("cajon");
            entity.Property(e => e.Cierre).HasColumnName("cierre");
            entity.Property(e => e.Descuentocanalventa).HasColumnName("descuentocanalventa");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idcanalventa).HasColumnName("idcanalventa");
            entity.Property(e => e.Mesa).HasColumnName("mesa");
            entity.Property(e => e.Mesero)
                .HasMaxLength(50)
                .HasColumnName("mesero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .HasColumnName("observaciones");
            entity.Property(e => e.Pagada).HasColumnName("pagada");
            entity.Property(e => e.Personas).HasColumnName("personas");
            entity.Property(e => e.Ticket).HasColumnName("ticket");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("pago", "fdw_restaurante");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Fecha)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idorden).HasColumnName("idorden");
            entity.Property(e => e.Metodo)
                .HasColumnType("character varying")
                .HasColumnName("metodo");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");
            entity.Property(e => e.Referencia)
                .HasMaxLength(100)
                .HasColumnName("referencia");
        });

        modelBuilder.Entity<Pago1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("pago", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("perfil", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("producto", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Actualizado)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actualizado");
            entity.Property(e => e.Descripcioncorta)
                .HasMaxLength(500)
                .HasColumnName("descripcioncorta");
            entity.Property(e => e.Descripcionextensa)
                .HasMaxLength(5000)
                .HasColumnName("descripcionextensa");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Preciocosto).HasColumnName("preciocosto");
            entity.Property(e => e.Precioventa).HasColumnName("precioventa");
            entity.Property(e => e.Registro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registro");
        });

        modelBuilder.Entity<ProductoCanalVenta>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("productocanalventa", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Idcanalventa).HasColumnName("idcanalventa");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<Promocion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("promocion", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Fin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fin");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Inicio)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Porcantidad).HasColumnName("porcantidad");
            entity.Property(e => e.Porprecio).HasColumnName("porprecio");
            entity.Property(e => e.Precioventa).HasColumnName("precioventa");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("usuario", "fdw_catalogos");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(4)
                .HasColumnName("codigo");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Idperfil).HasColumnName("idperfil");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Registro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registro");
            entity.Property(e => e.Ultimaconexion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultimaconexion");
        });

        modelBuilder.Entity<ReporteVentas>(entity =>
        {
            entity.HasNoKey(); // 👈 necesario
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
