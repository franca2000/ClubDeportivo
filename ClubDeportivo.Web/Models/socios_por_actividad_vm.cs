namespace ClubDeportivo.Web.Models.ViewModels
{
    // ViewModel usado para el reporte de socios por actividad
    public class SociosPorActividadVM
    {
        public int ActividadId { get; set; }

        public string NombreActividad { get; set; } = string.Empty;

        public bool Activa { get; set; }

        public int Cupo { get; set; }

        // Cantidad de socios inscriptos en esa actividad
        public int SociosInscriptos { get; set; }

        // Propiedad calculada: cupos libres = cupo total - inscriptos (no negativa)
        public int CuposDisponibles => System.Math.Max(0, Cupo - SociosInscriptos);

        // Texto preparado para la vista
        public string ActivaTexto => Activa ? "Sí" : "No";

        // Porcentaje ocupado (guardando división por cero), con 1 decimal
        public double PorcentajeOcupado => Cupo == 0 ? 0 : System.Math.Round((SociosInscriptos / (double)Cupo) * 100.0, 1);
    }
}

