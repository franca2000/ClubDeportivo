using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClubDeportivo.Web.Models.ViewModels
{
    public class InscripcionCreateVM
    {
        [Required]
        public int SocioId { get; set; }

        [Required(ErrorMessage = "Seleccione una actividad")]
        public int ActividadId { get; set; }

        public string? NombreSocio { get; set; }
        public IEnumerable<SelectListItem> ActividadesDisponibles { get; set; } = new List<SelectListItem>();
    }
}
