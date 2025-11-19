using System.ComponentModel.DataAnnotations;

namespace ClubDeportivo.Web.Models
{
    // Representa la inscripción de un socio en una actividad del club
    public class Inscripcion
    {
        // Clave primaria de la tabla Inscripciones
        public int InscripcionId { get; set; }

        // Clave foránea al socio inscrito (obligatoria)
        [Required]
        public int SocioId { get; set; }

        // Navegación al objeto Socio (relación N:1)
        public Socio? Socio { get; set; }

        // Clave foránea a la actividad seleccionada (obligatoria)
        [Required]
        public int ActividadId { get; set; }

        // Navegación al objeto Actividad (relación N:1)
        public Actividad? Actividad { get; set; }

        // Fecha en la que se realiza la inscripción (por defecto, la fecha actual)
        [DataType(DataType.Date)]
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow.Date;
    }
}

