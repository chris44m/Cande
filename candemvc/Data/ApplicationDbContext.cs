using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CandelariaP.Models;

namespace CandelariaP.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asiento> Asientos { get; set; }

    public virtual DbSet<Calle> Calles { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Compradore> Compradores { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=candeprueba;user=root;pwd=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Asiento>(entity =>
        {
            entity.HasKey(e => e.IdAsiento).HasName("PRIMARY");

            entity.ToTable("asientos");

            entity.HasIndex(e => e.IdCalle, "ID_Calle");

            entity.HasIndex(e => e.IdZona, "ID_Zona");

            entity.Property(e => e.IdAsiento).HasColumnName("ID_Asiento");
            entity.Property(e => e.IdCalle).HasColumnName("ID_Calle");
            entity.Property(e => e.IdZona).HasColumnName("ID_Zona");

            entity.HasOne(d => d.IdCalleNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdCalle)
                .HasConstraintName("asientos_ibfk_2");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("asientos_ibfk_1");
        });

        modelBuilder.Entity<Calle>(entity =>
        {
            entity.HasKey(e => e.IdCalle).HasName("PRIMARY");

            entity.ToTable("calles");

            entity.Property(e => e.IdCalle).HasColumnName("ID_Calle");
            entity.Property(e => e.NombreCalle)
                .HasMaxLength(255)
                .HasColumnName("Nombre_Calle");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PRIMARY");

            entity.ToTable("compras");

            entity.HasIndex(e => e.IdAsiento, "ID_Asiento");

            entity.HasIndex(e => e.IdCalle, "ID_Calle");

            entity.HasIndex(e => e.IdComprador, "ID_Comprador");

            entity.HasIndex(e => e.IdZona, "ID_Zona");

            entity.Property(e => e.IdCompra).HasColumnName("ID_Compra");
            entity.Property(e => e.FechaCompra)
                .HasColumnType("timestamp")
                .HasColumnName("Fecha_Compra");
            entity.Property(e => e.IdAsiento).HasColumnName("ID_Asiento");
            entity.Property(e => e.IdCalle).HasColumnName("ID_Calle");
            entity.Property(e => e.IdComprador).HasColumnName("ID_Comprador");
            entity.Property(e => e.IdZona).HasColumnName("ID_Zona");
            entity.Property(e => e.ImagenQr)
                .HasMaxLength(255)
                .HasColumnName("Imagen_QR");

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdAsiento)
                .HasConstraintName("compras_ibfk_1");

            entity.HasOne(d => d.IdCalleNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdCalle)
                .HasConstraintName("compras_ibfk_3");

            entity.HasOne(d => d.IdCompradorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdComprador)
                .HasConstraintName("compras_ibfk_4");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("compras_ibfk_2");
        });

        modelBuilder.Entity<Compradore>(entity =>
        {
            entity.HasKey(e => e.IdComprador).HasName("PRIMARY");

            entity.ToTable("compradores");

            entity.Property(e => e.IdComprador).HasColumnName("ID_Comprador");
            entity.Property(e => e.ApellidoComprador)
                .HasMaxLength(255)
                .HasColumnName("Apellido_Comprador");
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.NombreComprador)
                .HasMaxLength(255)
                .HasColumnName("Nombre_Comprador");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRol).HasColumnName("ID_Rol");
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.NombreRol)
                .HasMaxLength(255)
                .HasColumnName("Nombre_Rol");
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.IdZona).HasName("PRIMARY");

            entity.ToTable("zonas");

            entity.HasIndex(e => e.IdCalle, "ID_Calle");

            entity.Property(e => e.IdZona).HasColumnName("ID_Zona");
            entity.Property(e => e.IdCalle).HasColumnName("ID_Calle");
            entity.Property(e => e.NombreZona)
                .HasMaxLength(255)
                .HasColumnName("Nombre_Zona");
            entity.Property(e => e.Precio).HasPrecision(10, 2);

            entity.HasOne(d => d.IdCalleNavigation).WithMany(p => p.Zonas)
                .HasForeignKey(d => d.IdCalle)
                .HasConstraintName("zonas_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
