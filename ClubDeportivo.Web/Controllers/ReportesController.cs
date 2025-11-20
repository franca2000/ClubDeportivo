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

        // GET: /Reportes/ExportarSociosPorActividad
        public async Task<IActionResult> ExportarSociosPorActividad()
        {
            var datos = await _ctx.Actividades
                .OrderBy(a => a.Nombre)
                .Select(a => new SociosPorActividadVM
                {
                    ActividadId = a.ActividadId,
                    NombreActividad = a.Nombre,
                    Activa = a.Activo,
                    Cupo = a.Cupo,
                    SociosInscriptos = _ctx.Inscripciones.Count(i => i.ActividadId == a.ActividadId)
                })
                .ToListAsync();

            static string EscapeCsv(string? s)
            {
                if (string.IsNullOrEmpty(s)) return "";
                // Dobla comillas y encierra en comillas. Usamos punto y coma como separador (Excel en locales ES).
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            }

            var sb = new StringBuilder();
            // Encabezado en español (separador: ;)
            sb.AppendLine("Actividad;Activa;Cupo;Socios inscriptos;Cupos disponibles");

            foreach (var item in datos)
            {
                // Usamos ; como separador (mejor compatibilidad con Excel en locales que usan coma decimal)
                sb.Append(EscapeCsv(item.NombreActividad));
                sb.Append(';');
                sb.Append(item.Activa ? "Sí" : "No");
                sb.Append(';');
                sb.Append(item.Cupo);
                sb.Append(';');
                sb.Append(item.SociosInscriptos);
                sb.Append(';');
                sb.Append(item.CuposDisponibles);
                sb.AppendLine();
            }

            // UTF-8 con BOM para que Excel lo abra correctamente
            var bytes = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true).GetBytes(sb.ToString());
            var fileName = "socios_por_actividad.csv";
            return File(bytes, "text/csv; charset=utf-8", fileName);
        }
    }
}
