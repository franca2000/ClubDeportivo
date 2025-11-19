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
    // Controlador encargado de la gestión de socios (CRUD)
    public class SociosController : Controller
    {
        private readonly AppDbContext _context;

        // Inyección del contexto de base de datos
        public SociosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Socios
        // Muestra la lista completa de socios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Socios.ToListAsync());
        }

        // GET: Socios/Details/5
        // Muestra los detalles de un socio según su ID
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Busca el socio en la base de datos
            var socio = await _context.Socios
                .FirstOrDefaultAsync(m => m.SocioId == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // GET: Socios/Create
        // Devuelve el formulario para crear un nuevo socio
        public IActionResult Create()
        {
            return View();
        }

        // POST: Socios/Create
        // Crea un nuevo socio con validación y protección CSRF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SocioId,Dni,Nombre,Apellido,FechaNacimiento,Email,Telefono,Direccion")] Socio socio)
        {
            if (ModelState.IsValid)
            {
                // Si tu modelo tiene FechaAlta y querés setearla automáticamente, podrías hacer:
                // socio.FechaAlta = DateTime.Today;

                _context.Add(socio);               // Agrega el socio al contexto
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, vuelve a mostrar el formulario con errores
            return View(socio);
        }

        // GET: Socios/Edit/5
        // Muestra el formulario para editar un socio existente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios.FindAsync(id);
            if (socio == null)
            {
                return NotFound();
            }
            return View(socio);
        }

        // POST: Socios/Edit/5
        // Actualiza los datos de un socio existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SocioId,Dni,Nombre,Apellido,FechaNacimiento,Email,Telefono,Direccion")] Socio socio)
        {
            if (id != socio.SocioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocioExists(socio.SocioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(socio);
        }

        // GET: Socios/Delete/5
        // Muestra la confirmación para eliminar un socio
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .FirstOrDefaultAsync(m => m.SocioId == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // POST: Socios/Delete/5
        // Elimina definitivamente el socio
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var socio = await _context.Socios.FindAsync(id);
            if (socio != null)
            {
                _context.Socios.Remove(socio);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar privado para chequear si existe un socio por ID
        private bool SocioExists(int id)
        {
            return _context.Socios.Any(e => e.SocioId == id);
        }
    }
}

