using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceAlmacen.Models;

public partial class MiAlmacenContext : DbContext
{
    public MiAlmacenContext(DbContextOptions<MiAlmacenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Familia> Familias { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Operacione> Operaciones { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Familia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Familias__3214EC073E420401");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Operacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operacio__3214EC07CB9AD11E");

            entity.Property(e => e.Controller).HasMaxLength(50);
            entity.Property(e => e.FechaAccion).HasColumnType("datetime");
            entity.Property(e => e.Ip).HasMaxLength(50);
            entity.Property(e => e.Operacion).HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC07338B4261");

            entity.Property(e => e.FotoUrl).HasColumnName("FotoURL");
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Precio).HasColumnType("decimal(9, 2)");

            entity.HasOne(d => d.Familia).WithMany(p => p.Productos)
                .HasForeignKey(d => d.FamiliaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Familias_Productos");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC078DE91E27");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
