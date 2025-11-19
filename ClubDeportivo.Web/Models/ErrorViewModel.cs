namespace ClubDeportivo.Web.Models
{
    // Modelo utilizado para mostrar información en la vista de errores
    public class ErrorViewModel
    {
        // Identificador único de la solicitud actual (útil para rastrear errores)
        public string? RequestId { get; set; }

        // Propiedad calculada: indica si debe mostrarse el RequestId en la vista
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

