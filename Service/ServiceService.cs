using AppTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppTareas.Service
{
    public class ServiceService : Controller
    {
        private readonly ServiceContext _db;
        
        public ServiceService(ServiceContext db)
        {
            _db = db;
        }
        public List<Servicio> ObtenerServicios()
        {
            var idUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
            int idUsuario = int.Parse(idUsuarioClaim.Value);
            var datos = _db.Servicios
                .Where(e => e.IdUsuario == idUsuario)
                .ToList();
            return datos;
        }
    }
}
