using System;
using System.Collections.Generic;

namespace AppTareas.Models
{
    public partial class TiposServicio
    {
        public TiposServicio()
        {
            Servicios = new HashSet<Servicio>();
        }

        public int IdTipoServicio { get; set; }
        public string NombreTipo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal? CostoBase { get; set; }
        public string Modalidad { get; set; } = null!;

        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}
