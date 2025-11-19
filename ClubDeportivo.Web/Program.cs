using System;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using ClubDeportivo.Web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =============================================
// CONFIGURACIÓN DE SERVICIOS
// =============================================

// Habilita MVC + Razor
builder.Services.AddControllersWithViews();

// Configura Entity Framework Core con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Muestra errores útiles de EF en modo desarrollo
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// =============================================
// INICIALIZACIÓN DE BASE DE DATOS (MIGRACIONES)
// =============================================

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var ctx = services.GetRequiredService<AppDbContext>();

    // Aplica migraciones pendientes automáticamente
    ctx.Database.Migrate();
}

// =============================================
// MIDDLEWARE
// =============================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// =============================================
// ENDPOINTS / RUTEO
// =============================================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// =============================================
// EJECUCIÓN DE LA APLICACIÓN
// =============================================

app.Run();

