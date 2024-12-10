namespace AppTareas.Models.View_models
{
    public class TiposServiciosViewModel
    {
        public int IdUsuario { get; set; }
        public int IdTipoServicio { get; set; }

        public string Titulo { get; set; } = null!;
        public string NombreTipo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Presupuesto { get; set; }
        public int? HorasEstimadas { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Estado { get; set; } = null!;

    }
}
