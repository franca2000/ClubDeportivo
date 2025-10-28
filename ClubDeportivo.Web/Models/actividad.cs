using System.ComponentModel.DataAnnotations;

namespace ClubDeportivo.Web.Models
{
    public class Actividad
    {
        public int ActividadId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Debe indicar al menos un día")]
        public string Dias { get; set; } = string.Empty; // Ej: "Lu,Mi,Vi"

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [CustomValidation(typeof(Validations), nameof(Validations.HoraFinMayorQueInicio))]
        public TimeSpan HoraFin { get; set; }

        [Range(1, int.MaxValue)]
        public int Cupo { get; set; }

        public bool Activo { get; set; } = true;

        // Relación con Inscripciones
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}
