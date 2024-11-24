using System;
using System.Collections.Generic;

namespace AppTareas.Models
{
    public partial class Comentario
    {
        public int IdComentario { get; set; }
        public int IdUsuario { get; set; }
        public int IdServicio { get; set; }
        public string Comentario1 { get; set; } = null!;
        public DateTime FechaComentario { get; set; }

        public virtual Servicio IdServicioNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
