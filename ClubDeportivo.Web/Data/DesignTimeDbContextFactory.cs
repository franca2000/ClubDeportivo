using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ClubDeportivo.Web.Data
{
    // Fábrica de contexto utilizada exclusivamente en tiempo de diseño
    // (por ejemplo, para ejecutar comandos de Entity Framework como "Add-Migration" o "Update-Database")
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        // Método requerido por EF Core para crear una instancia del contexto sin ejecutar la aplicación
        public AppDbContext CreateDbContext(string[] args)
        {
            // Carga la configuración desde appsettings.json (donde está la cadena de conexión)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Directorio actual del proyecto
                .AddJsonFile("appsettings.json", optional: false) // Archivo obligatorio
                .Build();

            // Obtiene la cadena de conexión llamada "DefaultConnection"
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configura las opciones del contexto para usar SQL Server con esa conexión
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // Devuelve una nueva instancia del contexto con las opciones configuradas
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

