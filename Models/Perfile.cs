using System;
using System.Collections.Generic;

namespace AppTareas.Models
{
    public partial class Perfile
    {
        public int IdPerfil { get; set; }
        public int IdUsuario { get; set; }
        public string? Descripcion { get; set; }
        public string? Experiencia { get; set; }
        public string? Habilidades { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
