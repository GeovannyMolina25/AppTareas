using System;
using System.Collections.Generic;

namespace AppTareas.Models
{
    public partial class Puja
    {
        public int IdPuja { get; set; }
        public int IdServicio { get; set; }
        public int IdUsuario { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPuja { get; set; }

        public virtual Servicio IdServicioNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
