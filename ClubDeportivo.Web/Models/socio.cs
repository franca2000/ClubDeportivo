using System.ComponentModel.DataAnnotations;

namespace ClubDeportivo.Web.Models
{
    public class Socio
    {
        public int SocioId { get; set; }

        [Required]
        [Range(1, 99999999, ErrorMessage = "DNI inválido")]
        public int Dni { get; set; }

        [Required, MinLength(2)]
        public string Nombre { get; set; } = string.Empty;

        [Required, MinLength(2)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Validations), nameof(Validations.FechaNoFutura))]
        public DateTime FechaNacimiento { get; set; }

        [EmailAddress] public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
        public string NombreCompleto => $"{Apellido}, {Nombre}";
    }
}
