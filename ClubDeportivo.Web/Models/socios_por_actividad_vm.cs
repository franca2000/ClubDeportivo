namespace ClubDeportivo.Web.Models.ViewModels
{
    // ViewModel usado para el reporte de socios por actividad
    public class SociosPorActividadVM
    {
        public int ActividadId { get; set; }

        public string NombreActividad { get; set; } = null!;

        public bool Activa { get; set; }

        public int Cupo { get; set; }

        // Cantidad de socios inscriptos en esa actividad
        public int SociosInscriptos { get; set; }

        // Propiedad calculada: cupos libres = cupo total - inscriptos
        public int CuposDisponibles => Cupo - SociosInscriptos;
    }
}

