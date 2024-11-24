using System;
using System.Collections.Generic;

namespace AppTareas.Models
{
    public partial class Transaccione
    {
        public int IdTransaccion { get; set; }
        public int IdServicio { get; set; }
        public int IdUsuarioCliente { get; set; }
        public int IdUsuarioProveedor { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaTransaccion { get; set; }

        public virtual Servicio IdServicioNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioClienteNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioProveedorNavigation { get; set; } = null!;
    }
}
