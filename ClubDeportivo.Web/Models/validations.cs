using System.ComponentModel.DataAnnotations;

namespace ClubDeportivo.Web.Models
{
    public static class Validations
    {
        public static ValidationResult? FechaNoFutura(DateTime fecha, ValidationContext _)
            => fecha <= DateTime.Today ? ValidationResult.Success : new ValidationResult("La fecha no puede ser futura");

        public static ValidationResult? HoraFinMayorQueInicio(TimeSpan horaFin, ValidationContext ctx)
        {
            var actividad = ctx.ObjectInstance as Actividad;
            if (actividad == null) return ValidationResult.Success;
            return (horaFin > actividad.HoraInicio)
                ? ValidationResult.Success
                : new ValidationResult("Hora fin debe ser mayor a hora inicio");
        }
    }
}
