using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRMBASEDEDATOS.Models;

public partial class CrmContext : DbContext
{
    public CrmContext()
    {
    }

    public CrmContext(DbContextOptions<CrmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<InscripcionCurso> InscripcionCursos { get; set; }

    public virtual DbSet<Promocione> Promociones { get; set; }

    public virtual DbSet<Tratamiento> Tratamientos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            //        => optionsBuilder.UseSqlServer("server=localhost\\MSSQLSERVER01 ;Database=CRM; integrated security=True; TrustServerCertificate=True;");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Citas__7C17FD16CAB4B81D");

            entity.Property(e => e.IdCita).HasColumnName("ID_Cita");
            entity.Property(e => e.Estatus).HasMaxLength(15);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.IdPromocion).HasColumnName("ID_Promocion");
            entity.Property(e => e.IdTratamiento).HasColumnName("ID_Tratamiento");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__ID_Client__4316F928");

            entity.HasOne(d => d.IdPromocionNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPromocion)
                .HasConstraintName("FK__Citas__ID_Promoc__44FF419A");

            entity.HasOne(d => d.IdTratamientoNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdTratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__ID_Tratam__440B1D61");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__E005FBFF71AA6669");

            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__Cursos__DC72196F592AF55C");

            entity.Property(e => e.IdCurso).HasColumnName("ID_Curso");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Estatus).HasMaxLength(15);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<InscripcionCurso>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PK__Inscripc__B84666E0459ECD9F");

            entity.ToTable("InscripcionCurso");

            entity.Property(e => e.IdInscripcion).HasColumnName("ID_Inscripcion");
            entity.Property(e => e.Estatus).HasMaxLength(15);
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.IdCurso).HasColumnName("ID_Curso");
            entity.Property(e => e.PrecioTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.InscripcionCursos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__ID_Cl__48CFD27E");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.InscripcionCursos)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__ID_Cu__49C3F6B7");
        });

        modelBuilder.Entity<Promocione>(entity =>
        {
            entity.HasKey(e => e.IdPromocion).HasName("PK__Promocio__577F2CA68803C934");

            entity.Property(e => e.IdPromocion).HasColumnName("ID_Promocion");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Estatus).HasMaxLength(15);
            entity.Property(e => e.IdTratamiento).HasColumnName("ID_Tratamiento");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.IdTratamientoNavigation).WithMany(p => p.Promociones)
                .HasForeignKey(d => d.IdTratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Promocion__ID_Tr__3C69FB99");
        });

        modelBuilder.Entity<Tratamiento>(entity =>
        {
            entity.HasKey(e => e.IdTratamiento).HasName("PK__Tratamie__37F4ED153735EB48");

            entity.Property(e => e.IdTratamiento).HasColumnName("ID_Tratamiento");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
