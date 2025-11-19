using ClubDeportivo.Web.Data;
using ClubDeportivo.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubDeportivo.Web.Controllers
{
    public class ReportesController : Controller
    {
        private readonly AppDbContext _ctx;

        public ReportesController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        // GET: /Reportes/SociosPorActividad
        public async Task<IActionResult> SociosPorActividad()
        {
            // Para cada actividad, armamos un ViewModel que incluye:
            // - datos de la actividad
            // - cantidad de socios inscriptos (consultando la tabla Inscripciones)
            var datos = await _ctx.Actividades
                .OrderBy(a => a.Nombre)
                .Select(a => new SociosPorActividadVM
                {
                    ActividadId = a.ActividadId,
                    NombreActividad = a.Nombre,
                    Activa = a.Activo,
                    Cupo = a.Cupo,

                    // AQUÍ está la “conexión” con Inscripciones:
                    SociosInscriptos = _ctx.Inscripciones
                        .Count(i => i.ActividadId == a.ActividadId)
                })
                .ToListAsync();

            return View(datos);
        }
    }
}
