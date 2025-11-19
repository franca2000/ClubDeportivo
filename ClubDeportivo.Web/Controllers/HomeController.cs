using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClubDeportivo.Web.Models;

namespace ClubDeportivo.Web.Controllers
{
    // Controlador principal del sitio (páginas públicas o base del proyecto)
    public class HomeController : Controller
    {
        // Inyección de dependencia para el sistema de logging (registros)
        private readonly ILogger<HomeController> _logger;

        // Constructor: recibe el logger y lo asigna al campo local
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acción principal del sitio: retorna la vista "Index"
        // Corresponde a la página de inicio (por defecto /Home/Index)
        public IActionResult Index()
        {
            return View();
        }

        // Acción que muestra la vista de política de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción que se ejecuta cuando ocurre un error en la aplicación
        // [ResponseCache] evita que se almacene la respuesta en caché
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Crea un modelo de error con el identificador de la solicitud actual
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
