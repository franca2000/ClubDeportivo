using System.ComponentModel.DataAnnotations;

namespace ClubDeportivo.Web.Models
{
    // Clase estática que contiene validaciones personalizadas reutilizables para los modelos
    public static class Validations
    {
        // ✅ Valida que una fecha no sea futura (usada en Socio.FechaNacimiento)
        public static ValidationResult? FechaNoFutura(DateTime fecha, ValidationContext _)
            => fecha <= DateTime.Today
                ? ValidationResult.Success
                : new ValidationResult("La fecha no puede ser futura");

        // ✅ Valida que la hora de finalización sea mayor que la de inicio (usada en Actividad)
        public static ValidationResult? HoraFinMayorQueInicio(TimeSpan horaFin, ValidationContext ctx)
        {
            // Obtiene la instancia actual de la clase que se está validando
            var actividad = ctx.ObjectInstance as Actividad;
            if (actividad == null) return ValidationResult.Success;

            // Verifica que la hora de fin sea posterior a la hora de inicio
            return (horaFin > actividad.HoraInicio)
                ? ValidationResult.Success
                : new ValidationResult("Hora fin debe ser mayor a hora inicio");
        }
    }
}

