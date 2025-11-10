using System;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using System.Linq;
using ClubDeportivo.Web.Data;
using ClubDeportivo.Web.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC + Razor Views
builder.Services.AddControllersWithViews();

// EF Core: SQL Server (DefaultConnection en appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// (Opcional) Muestra detalles de errores de DB en Development
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// ====== DB: aplicar migraciones y seed ======
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var ctx = services.GetRequiredService<AppDbContext>();

    // Aplica migraciones pendientes (crea la BD si no existe)
    ctx.Database.Migrate();

    // Seed simple (solo si la tabla está vacía)
    if (!ctx.Actividades.Any())
    {
        ctx.Actividades.AddRange(
            new Actividad { Nombre = "Fútbol", Descripcion = "Mixto", Dias = "Lu,Mi,Vi", HoraInicio = new TimeSpan(18, 0, 0), HoraFin = new TimeSpan(19, 30, 0), Cupo = 20, Activo = true },
            new Actividad { Nombre = "Natación", Descripcion = "Pileta", Dias = "Ma,Jue", HoraInicio = new TimeSpan(17, 0, 0), HoraFin = new TimeSpan(18, 0, 0), Cupo = 12, Activo = true },
            new Actividad { Nombre = "Spinning", Descripcion = "Indoor", Dias = "Sa", HoraInicio = new TimeSpan(10, 0, 0), HoraFin = new TimeSpan(11, 0, 0), Cupo = 10, Activo = true }
        );
    }

    if (!ctx.Socios.Any())
    {
        ctx.Socios.AddRange(
            new Socio { Dni = 12345678, Nombre = "Ana", Apellido = "García", FechaNacimiento = new DateTime(1995, 5, 10), Email = "ana@club.com" },
            new Socio { Dni = 23456789, Nombre = "Luis", Apellido = "Pérez", FechaNacimiento = new DateTime(1990, 2, 20), Email = "luis@club.com" },
            new Socio { Dni = 34567890, Nombre = "Sofía", Apellido = "López", FechaNacimiento = new DateTime(2000, 9, 1), Email = "sofia@club.com" }
        );
    }

    ctx.SaveChanges();
}
// ============================================

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


