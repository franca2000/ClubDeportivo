using ClubDeportivo.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubDeportivo.Web.Controllers
{
    public class ReportesController : Controller
    {
        private readonly AppDbContext _ctx;
        public ReportesController(AppDbContext ctx) => _ctx = ctx;

        // /Reportes/SociosPorActividad
        public async Task<IActionResult> SociosPorActividad()
        {
            var data = await _ctx.Actividades
                .IgnoreQueryFilters() // incluir inactivas para comparar
                .Select(a => new
                {
                    a.Nombre,
                    a.Activo,
                    TotalSocios = _ctx.Inscripciones.Count(i => i.ActividadId == a.ActividadId)
                })
                .OrderByDescending(x => x.TotalSocios)
                .ToListAsync();

            return View(data);
        }
    }
}
