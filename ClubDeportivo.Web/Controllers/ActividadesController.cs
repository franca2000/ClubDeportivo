using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClubDeportivo.Web.Data;
using ClubDeportivo.Web.Models;

namespace ClubDeportivo.Web.Controllers
{
    // Controlador MVC para CRUD de Actividades
    public class ActividadesController : Controller
    {
        private readonly AppDbContext _context;

        // Inyección de dependencias del DbContext
        public ActividadesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Actividades
        // Lista todas las actividades
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actividades.ToListAsync());
        }

        // GET: Actividades/Details/5
        // Muestra el detalle de una actividad por id
        public async Task<IActionResult> Details(int? id)
        {
            // Valida parámetro requerido
            if (id == null)
            {
                return NotFound();
            }

            // Busca la actividad; si no existe, 404
            var actividad = await _context.Actividades
                .FirstOrDefaultAsync(m => m.ActividadId == id);
            if (actividad == null)
            {
                return NotFound();
            }

            return View(actividad);
        }

        // GET: Actividades/Create
        // Devuelve el formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actividades/Create
        // Crea una nueva actividad con los campos permitidos (Bind) y valida CSRF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActividadId,Nombre,Descripcion,Dias,HoraInicio,HoraFin,Cupo,Activo")] Actividad actividad)
        {
            // Valida reglas de modelo (DataAnnotations, etc.)
            if (ModelState.IsValid)
            {
                _context.Add(actividad);
                await _context.SaveChangesAsync(); // Persiste en DB
                return RedirectToAction(nameof(Index)); // Vuelve al listado
            }
            // Si hay errores de validación, vuelve al formulario con el modelo
            return View(actividad);
        }

        // GET: Actividades/Edit/5
        // Devuelve el formulario de edición precargado
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Busca por clave primaria
            var actividad = await _context.Actividades.FindAsync(id);
            if (actividad == null)
            {
                return NotFound();
            }
            return View(actividad);
        }

        // POST: Actividades/Edit/5
        // Actualiza la actividad; maneja concurrencia optimista y valida CSRF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActividadId,Nombre,Descripcion,Dias,HoraInicio,HoraFin,Cupo,Activo")] Actividad actividad)
        {
            // Evita edición de un id distinto al del modelo
            if (id != actividad.ActividadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actividad);          // Marca entidad como modificada
                    await _context.SaveChangesAsync();    // Intenta guardar cambios
                }
                catch (DbUpdateConcurrencyException)       // Colisión de concurrencia
                {
                    // Si ya no existe, 404; si existe, relanzar la excepción
                    if (!ActividadExists(actividad.ActividadId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Edición exitosa: volver al listado
                return RedirectToAction(nameof(Index));
            }
            // Si hay errores de validación, re-mostrar formulario
            return View(actividad);
        }

        // GET: Actividades/Delete/5
        // Muestra confirmación de borrado
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividades
                .FirstOrDefaultAsync(m => m.ActividadId == id);
            if (actividad == null)
            {
                return NotFound();
            }

            return View(actividad);
        }

        // POST: Actividades/Delete/5
        // Ejecuta el borrado confirmado; valida CSRF
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Busca y, si existe, elimina la entidad
            var actividad = await _context.Actividades.FindAsync(id);
            if (actividad != null)
            {
                _context.Actividades.Remove(actividad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper: verifica existencia por id (usado en manejo de concurrencia)
        private bool ActividadExists(int id)
        {
            return _context.Actividades.Any(e => e.ActividadId == id);
        }
    }
}
