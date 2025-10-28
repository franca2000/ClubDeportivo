using System.ComponentModel.DataAnnotations;

namespace ClubDeportivo.Web.Models
{
    public class Inscripcion
    {
        public int InscripcionId { get; set; }

        [Required] public int SocioId { get; set; }
        public Socio? Socio { get; set; }

        [Required] public int ActividadId { get; set; }
        public Actividad? Actividad { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow.Date;
    }
}
