using ClubDeportivo.Web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregamos servicios al contenedor (MVC + Razor Views)
builder.Services.AddControllersWithViews();

// Configuración del contexto de base de datos (EF Core)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Construcción de la aplicación
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ClubDeportivo.Web.Data.AppDbContext>();
    ctx.Database.EnsureCreated();

    if (!ctx.Actividades.Any())
    {
        ctx.Actividades.AddRange(
            new ClubDeportivo.Web.Models.Actividad { Nombre = "Fútbol", Descripcion = "Mixto", Dias = "Lu,Mi,Vi", HoraInicio = new TimeSpan(18, 0, 0), HoraFin = new TimeSpan(19, 30, 0), Cupo = 20, Activo = true },
            new ClubDeportivo.Web.Models.Actividad { Nombre = "Natación", Descripcion = "Pileta", Dias = "Ma,Jue", HoraInicio = new TimeSpan(17, 0, 0), HoraFin = new TimeSpan(18, 0, 0), Cupo = 12, Activo = true },
            new ClubDeportivo.Web.Models.Actividad { Nombre = "Spinning", Descripcion = "Indoor", Dias = "Sa", HoraInicio = new TimeSpan(10, 0, 0), HoraFin = new TimeSpan(11, 0, 0), Cupo = 10, Activo = true }
        );
    }

    if (!ctx.Socios.Any())
    {
        ctx.Socios.AddRange(
            new ClubDeportivo.Web.Models.Socio { Dni = 12345678, Nombre = "Ana", Apellido = "García", FechaNacimiento = new DateTime(1995, 5, 10), Email = "ana@club.com" },
            new ClubDeportivo.Web.Models.Socio { Dni = 23456789, Nombre = "Luis", Apellido = "Pérez", FechaNacimiento = new DateTime(1990, 2, 20), Email = "luis@club.com" },
            new ClubDeportivo.Web.Models.Socio { Dni = 34567890, Nombre = "Sofía", Apellido = "López", FechaNacimiento = new DateTime(2000, 9, 1), Email = "sofia@club.com" }
        );
    }

    ctx.SaveChanges();
}


// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Habilita Strict-Transport-Security
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rutas por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecutar la aplicación
app.Run();

