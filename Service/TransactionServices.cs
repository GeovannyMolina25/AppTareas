using AppTareas.Models;
using AppTareas.Models.View_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppTareas.Service
{
    public class TransactionServices : Controller
    {
        public readonly ServiceContext _db;

        public TransactionServices(ServiceContext db)
        {
            _db = db;
        }
        public List<TransaccionViewModel> ObtenerDatos()
        {
            var datos = _db.Transacciones
                .Include(t => t.IdServicioNavigation)
                .Include(t => t.IdUsuarioClienteNavigation)
                .GroupBy(e => new { e.IdServicioNavigation.Titulo, e.IdUsuarioClienteNavigation.Nombre }) // Agrupamos por ambos campos
                .Select(g => new TransaccionViewModel
                {
                    IdUsuarioCliente = g.FirstOrDefault().IdUsuarioClienteNavigation.IdUsuario, // Tomamos el primer IdUsuarioCliente del grupo
                    NombreUsuarioCliente = g.Key.Nombre, // Nombre del cliente, que es único en el grupo
                    IdServicio = g.FirstOrDefault().IdServicioNavigation.IdServicio, // Tomamos el primer IdServicio del grupo
                    NombreServicio = g.Key.Titulo // Nombre del servicio, que es único en el grupo
                })
                .ToList();

            return datos;
        }



    }
}
