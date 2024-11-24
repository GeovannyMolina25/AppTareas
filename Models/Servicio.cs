using System;
using System.Collections.Generic;

namespace AppTareas.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            Comentarios = new HashSet<Comentario>();
            Pujas = new HashSet<Puja>();
            Transacciones = new HashSet<Transaccione>();
        }

        public int IdServicio { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoServicio { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Presupuesto { get; set; }
        public int? HorasEstimadas { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Estado { get; set; } = null!;

        public virtual TiposServicio IdTipoServicioNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Puja> Pujas { get; set; }
        public virtual ICollection<Transaccione> Transacciones { get; set; }
    }
}
