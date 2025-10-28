using ClubDeportivo.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubDeportivo.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Socio> Socios => Set<Socio>();
        public DbSet<Actividad> Actividades => Set<Actividad>();
        public DbSet<Inscripcion> Inscripciones => Set<Inscripcion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Socio>()
                .HasIndex(s => s.Dni)
                .IsUnique();

            modelBuilder.Entity<Actividad>()
                .HasIndex(a => a.Nombre)
                .IsUnique();

            modelBuilder.Entity<Inscripcion>()
                .HasIndex(i => new { i.SocioId, i.ActividadId })
                .IsUnique();

            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Socio)
                .WithMany(s => s.Inscripciones)
                .HasForeignKey(i => i.SocioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Actividad)
                .WithMany(a => a.Inscripciones)
                .HasForeignKey(i => i.ActividadId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
