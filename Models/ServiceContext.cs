using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppTareas.Models
{
    public partial class ServiceContext : DbContext
    {
        public ServiceContext()
        {
        }

        public ServiceContext(DbContextOptions<ServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comentario> Comentarios { get; set; } = null!;
        public virtual DbSet<Perfile> Perfiles { get; set; } = null!;
        public virtual DbSet<Puja> Pujas { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<TiposServicio> TiposServicios { get; set; } = null!;
        public virtual DbSet<Transaccione> Transacciones { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario)
                    .HasName("PK__Comentar__DDBEFBF9A73B17AD");

                entity.Property(e => e.Comentario1).HasColumnName("Comentario");

                entity.Property(e => e.FechaComentario)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comentari__IdSer__5BE2A6F2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Comentari__IdUsu__5AEE82B9");
            });

            modelBuilder.Entity<Perfile>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("PK__Perfiles__C7BD5CC1BA238247");

                entity.Property(e => e.Experiencia).HasMaxLength(500);

                entity.Property(e => e.Habilidades).HasMaxLength(500);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Perfiles)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Perfiles__IdUsua__3E52440B");
            });

            modelBuilder.Entity<Puja>(entity =>
            {
                entity.HasKey(e => e.IdPuja)
                    .HasName("PK__Pujas__F986B2D4FC6ED45C");

                entity.Property(e => e.FechaPuja)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.Pujas)
                    .HasForeignKey(d => d.IdServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pujas__IdServici__5070F446");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pujas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pujas__IdUsuario__5165187F");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio)
                    .HasName("PK__Servicio__2DCCF9A29F6916C2");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaPublicacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Presupuesto).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Titulo).HasMaxLength(200);

                entity.HasOne(d => d.IdTipoServicioNavigation)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.IdTipoServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Servicios__IdTip__47DBAE45");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Servicios__IdUsu__46E78A0C");
            });

            modelBuilder.Entity<TiposServicio>(entity =>
            {
                entity.HasKey(e => e.IdTipoServicio)
                    .HasName("PK__TiposSer__E29B3EA7669B41FE");

                entity.Property(e => e.CostoBase).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Modalidad).HasMaxLength(20);

                entity.Property(e => e.NombreTipo).HasMaxLength(100);
            });

            modelBuilder.Entity<Transaccione>(entity =>
            {
                entity.HasKey(e => e.IdTransaccion)
                    .HasName("PK__Transacc__334B1F77C3654D55");

                entity.Property(e => e.FechaTransaccion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.Transacciones)
                    .HasForeignKey(d => d.IdServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__IdSer__5535A963");

                entity.HasOne(d => d.IdUsuarioClienteNavigation)
                    .WithMany(p => p.TransaccioneIdUsuarioClienteNavigations)
                    .HasForeignKey(d => d.IdUsuarioCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__IdUsu__5629CD9C");

                entity.HasOne(d => d.IdUsuarioProveedorNavigation)
                    .WithMany(p => p.TransaccioneIdUsuarioProveedorNavigations)
                    .HasForeignKey(d => d.IdUsuarioProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__IdUsu__571DF1D5");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__5B65BF97D96E1446");

                entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534024943F2")
                    .IsUnique();

                entity.Property(e => e.Apellido).HasMaxLength(100);

                entity.Property(e => e.Calificacion)
                    .HasColumnType("decimal(3, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Contrasena).HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Rol).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
