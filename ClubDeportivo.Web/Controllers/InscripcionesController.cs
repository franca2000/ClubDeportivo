using ClubDeportivo.Web.Data;
using ClubDeportivo.Web.Models;
using ClubDeportivo.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClubDeportivo.Web.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly AppDbContext _ctx;
        public InscripcionesController(AppDbContext ctx) => _ctx = ctx;

        // GET: /Inscripciones/Create?socioId=5
        public async Task<IActionResult> Create(int socioId)
        {
            var socio = await _ctx.Socios.FindAsync(socioId);
            if (socio == null) return NotFound();

            // actividades activas con cupo disponible
            var actividades = await _ctx.Actividades
                .Where(a => a.Activo)
                .Select(a => new
                {
                    a.ActividadId,
                    a.Nombre,
                    Inscritos = _ctx.Inscripciones.Count(i => i.ActividadId == a.ActividadId),
                    a.Cupo
                })
                .ToListAsync();

            var disponibles = actividades
                .Where(a => a.Cupo > a.Inscritos)
                .Select(a => new SelectListItem
                {
                    Value = a.ActividadId.ToString(),
                    Text = $"{a.Nombre} (cupos: {a.Cupo - a.Inscritos})"
                })
                .ToList();

            var vm = new InscripcionCreateVM
            {
                SocioId = socioId,
                NombreSocio = $"{socio.Apellido}, {socio.Nombre}",
                ActividadesDisponibles = disponibles
            };
            return View(vm);
        }

        // POST: /Inscripciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InscripcionCreateVM vm)
        {
            if (!ModelState.IsValid) return await Create(vm.SocioId);

            var socio = await _ctx.Socios.FindAsync(vm.SocioId);
            var actividad = await _ctx.Actividades
                .FirstOrDefaultAsync(a => a.ActividadId == vm.ActividadId && a.Activo);

            if (socio == null || actividad == null)
            {
                ModelState.AddModelError("", "Socio o actividad inválida.");
                return await Create(vm.SocioId);
            }

            bool yaInscripto = await _ctx.Inscripciones
                .AnyAsync(i => i.SocioId == vm.SocioId && i.ActividadId == vm.ActividadId);

            if (yaInscripto)
            {
                ModelState.AddModelError("", "El socio ya está inscripto en esa actividad.");
                return await Create(vm.SocioId);
            }

            int inscritos = await _ctx.Inscripciones
                .CountAsync(i => i.ActividadId == vm.ActividadId);

            if (inscritos >= actividad.Cupo)
            {
                ModelState.AddModelError("", "No hay cupos disponibles en la actividad seleccionada.");
                return await Create(vm.SocioId);
            }

            _ctx.Inscripciones.Add(new Inscripcion
            {
                SocioId = vm.SocioId,
                ActividadId = vm.ActividadId,
                FechaInscripcion = DateTime.Today
            });

            await _ctx.SaveChangesAsync();

            TempData["Ok"] = "Inscripción realizada correctamente.";
            return RedirectToAction("Details", "Socios", new { id = vm.SocioId });
        }

        // GET: /Inscripciones/ActividadesPorSocio/5
        public async Task<IActionResult> ActividadesPorSocio(int id)
        {
            var socio = await _ctx.Socios.FindAsync(id);
            if (socio == null) return NotFound();

            var actividades = await _ctx.Inscripciones
                .Where(i => i.SocioId == id)
                .Include(i => i.Actividad)
                .Select(i => new
                {
                    i.Actividad!.Nombre,
                    i.Actividad!.Dias,
                    i.Actividad!.HoraInicio,
                    i.Actividad!.HoraFin,
                    i.FechaInscripcion
                })
                .ToListAsync();

            ViewBag.Socio = $"{socio.Apellido}, {socio.Nombre}";
            return View(actividades);
        }
    }
}

