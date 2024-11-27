namespace AppTareas.Models.View_models
{
    public class TransaccionViewModel
    {
        public int IdTransaccion { get; set; }

        public int IdServicio { get; set; }
        public string NombreServicio { get; set; }

        public int IdUsuarioCliente { get; set; }
        public string NombreUsuarioCliente { get; set; }
        public int IdUsuarioProveedor { get; set; }
        public string NombreUsuarioProveedor { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaTransaccion { get; set; }


    }
}
