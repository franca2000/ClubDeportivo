using System.ComponentModel.DataAnnotations;

namespace ClubDeportivo.Web.Models
{
    // Representa una actividad del club (por ejemplo: fútbol, natación, yoga, etc.)
    public class Actividad
    {
        // Clave primaria de la tabla Actividades
        public int ActividadId { get; set; }

        // Nombre de la actividad (obligatorio)
        [Required]
        public string Nombre { get; set; } = string.Empty;

        // Descripción opcional de la actividad
        public string? Descripcion { get; set; }

        // Días en los que se dicta la actividad (ejemplo: "Lu,Mi,Vi")
        [Required]
        [MinLength(1, ErrorMessage = "Debe indicar al menos un día")]
        public string Dias { get; set; } = string.Empty;

        // Hora de inicio de la actividad (obligatorio)
        [Required]
        public TimeSpan HoraInicio { get; set; }

        // Hora de fin con validación personalizada (debe ser mayor que la hora de inicio)
        [Required]
        [CustomValidation(typeof(Validations), nameof(Validations.HoraFinMayorQueInicio))]
        public TimeSpan HoraFin { get; set; }

        // Cantidad máxima de participantes permitidos
        [Range(1, int.MaxValue)]
        public int Cupo { get; set; }

        // Indica si la actividad está activa o suspendida
        public bool Activo { get; set; } = true;

        // Relación 1:N -> una actividad puede tener muchas inscripciones
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}

