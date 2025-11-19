using ClubDeportivo.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubDeportivo.Web.Data
{
    // Contexto principal de la base de datos de la aplicación
    // Administra las entidades y sus relaciones usando Entity Framework Core
    public class AppDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración del contexto (cadena de conexión, proveedor, etc.)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablas (DbSets) que representan las entidades del modelo de datos
        public DbSet<Socio> Socios => Set<Socio>();
        public DbSet<Actividad> Actividades => Set<Actividad>();
        public DbSet<Inscripcion> Inscripciones => Set<Inscripcion>();

        // Configuración avanzada del modelo (índices, relaciones y restricciones)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Un DNI no puede repetirse entre socios
            modelBuilder.Entity<Socio>()
                .HasIndex(s => s.Dni)
                .IsUnique();

            // El nombre de una actividad debe ser único
            modelBuilder.Entity<Actividad>()
                .HasIndex(a => a.Nombre)
                .IsUnique();

            // Un socio no puede inscribirse dos veces en la misma actividad
            modelBuilder.Entity<Inscripcion>()
                .HasIndex(i => new { i.SocioId, i.ActividadId })
                .IsUnique();

            // Relación: un socio tiene muchas inscripciones
            // Si se elimina un socio, se eliminan sus inscripciones (Cascade)
            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Socio)
                .WithMany(s => s.Inscripciones)
                .HasForeignKey(i => i.SocioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación: una actividad tiene muchas inscripciones
            // No se permite eliminar una actividad si tiene inscripciones (Restrict)
            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Actividad)
                .WithMany(a => a.Inscripciones)
                .HasForeignKey(i => i.ActividadId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

