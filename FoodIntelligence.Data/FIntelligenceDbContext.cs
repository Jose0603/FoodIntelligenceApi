using System;
using System.Collections.Generic;
using FoodIntelligence.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodIntelligence.Data;

public partial class FIntelligenceDbContext : IdentityDbContext<AspNetUsers>
{
    public FIntelligenceDbContext()
    {
    }

    public FIntelligenceDbContext(DbContextOptions<FIntelligenceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alergia> Alergias { get; set; }

    public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

    public virtual DbSet<CategoriasComidum> CategoriasComida { get; set; }

    public virtual DbSet<ComidaIngrediente> ComidaIngredientes { get; set; }

    public virtual DbSet<Comidum> Comida { get; set; }

    public virtual DbSet<DetallesPedido> DetallesPedidos { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<MetodosDePago> MetodosDePagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Restaurante> Restaurantes { get; set; }

    public virtual DbSet<UsuariosAlergia> UsuariosAlergias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alergia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Alergias__3214EC27688C59B8");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<AspNetUsers>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<CategoriasComidum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27B834C86F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NombreCategoria).HasMaxLength(255);
        });

        modelBuilder.Entity<ComidaIngrediente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ComidaIn__3214EC27BEE491D6");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idcomida).HasColumnName("IDComida");
            entity.Property(e => e.Idingrediente).HasColumnName("IDIngrediente");

            entity.HasOne(d => d.IdcomidaNavigation).WithMany(p => p.ComidaIngredientes)
                .HasForeignKey(d => d.Idcomida)
                .HasConstraintName("FK_ComidaIngredientes_Comida");

            entity.HasOne(d => d.IdingredienteNavigation).WithMany(p => p.ComidaIngredientes)
                .HasForeignKey(d => d.Idingrediente)
                .HasConstraintName("FK_ComidaIngredientes_Ingredientes");
        });

        modelBuilder.Entity<Comidum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comida__3214EC278DCCB0B0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.Descuento).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Idrestaurante).HasColumnName("IDRestaurante");
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Comida)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK_Comida_CategoriasComida");

            entity.HasOne(d => d.IdrestauranteNavigation).WithMany(p => p.Comida)
                .HasForeignKey(d => d.Idrestaurante)
                .HasConstraintName("FK_Comida_Restaurantes");
        });

        modelBuilder.Entity<DetallesPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3214EC27B9C6A51B");

            entity.ToTable("DetallesPedido");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idcomida).HasColumnName("IDComida");
            entity.Property(e => e.Idpedido).HasColumnName("IDPedido");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdcomidaNavigation).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.Idcomida)
                .HasConstraintName("FK_DetallesPedido_Comida");

            entity.HasOne(d => d.IdpedidoNavigation).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.Idpedido)
                .HasConstraintName("FK_DetallesPedido_Pedidos");
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingredie__3214EC27B967E10E");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<MetodosDePago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MetodosD__3214EC2788AAFC53");

            entity.ToTable("MetodosDePago");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pedidos__3214EC27B59EA2C3");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DireccionEntrega).HasMaxLength(1000);
            entity.Property(e => e.EstadoPedido).HasMaxLength(255);
            entity.Property(e => e.FechaHoraPedido).HasColumnType("datetime");
            entity.Property(e => e.Idusuario)
                .HasMaxLength(450)
                .HasColumnName("IDUsuario");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("FK__Pedidos__IDUsuar__693CA210");

            entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Restaurante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3214EC271E60C9F0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DireccionRestaurante).HasMaxLength(255);
            entity.Property(e => e.Latitud).HasColumnType("decimal(10, 6)");
            entity.Property(e => e.Longitud).HasColumnType("decimal(10, 6)");
            entity.Property(e => e.NombreRestaurante).HasMaxLength(255);
            entity.Property(e => e.TelefonoRestaurante).HasMaxLength(20);
        });

        modelBuilder.Entity<UsuariosAlergia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC27D0CA2A18");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Idalergia).HasColumnName("IDAlergia");
            entity.Property(e => e.Idusuario)
                .HasMaxLength(450)
                .HasColumnName("IDUsuario");

            entity.HasOne(d => d.IdalergiaNavigation).WithMany(p => p.UsuariosAlergia)
                .HasForeignKey(d => d.Idalergia)
                .HasConstraintName("FK__UsuariosA__IDAle__367C1819");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.UsuariosAlergia)
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("FK__UsuariosA__IDUsu__3587F3E0");
        });

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
