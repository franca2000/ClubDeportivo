using System.ComponentModel.DataAnnotations;

namespace ClubDeportivo.Web.Models
{
    // Representa a un socio registrado en el club deportivo
    public class Socio
    {
        // Clave primaria de la tabla Socios
        public int SocioId { get; set; }

        // DNI del socio (obligatorio y dentro de un rango válido)
        [Required]
        [Range(1, 99999999, ErrorMessage = "DNI inválido")]
        public int Dni { get; set; }

        // Nombre del socio (mínimo 2 caracteres)
        [Required, MinLength(2)]
        public string Nombre { get; set; } = string.Empty;

        // Apellido del socio (mínimo 2 caracteres)
        [Required, MinLength(2)]
        public string Apellido { get; set; } = string.Empty;

        // Fecha de nacimiento (no puede ser futura)
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Validations), nameof(Validations.FechaNoFutura))]
        public DateTime FechaNacimiento { get; set; }

        // Datos de contacto opcionales
        [EmailAddress]
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }

        // Relación 1:N -> un socio puede tener muchas inscripciones
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();

        // Propiedad calculada que devuelve el nombre completo en formato "Apellido, Nombre"
        public string NombreCompleto => $"{Apellido}, {Nombre}";
    }
}


