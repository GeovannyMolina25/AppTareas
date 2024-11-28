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
                .GroupBy(e => new { e.IdServicioNavigation.Titulo, e.IdUsuarioClienteNavigation.Nombre }) 
                .Select(g => new TransaccionViewModel
                {
                    IdUsuarioCliente = g.FirstOrDefault().IdUsuarioClienteNavigation.IdUsuario,
                    IdServicio = g.FirstOrDefault().IdServicioNavigation.IdServicio, 
                    NombreServicio = g.Key.Titulo 
                })
                .ToList();

            return datos;
        }



    }
}
