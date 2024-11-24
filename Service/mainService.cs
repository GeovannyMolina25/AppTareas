using AppTareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppTareas.Service
{
    public class mainService : Controller
    {
        private readonly ServiceContext _db;

        public mainService(ServiceContext db)
        {
            _db = db;
        }
        

        public List<Comentario> ObtenerComentario()
        {
            var datos = _db.Comentarios
                .ToList();

            return datos;
        }

    }
}
