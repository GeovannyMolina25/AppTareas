using AppTareas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppTareas.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPractica : ControllerBase
    {
        private readonly ServiceContext _db;

        public ApiPractica(ServiceContext db)
        {
            _db = db;
        }
        // Api para los usuarios
        [HttpGet("Usuarios")]
        public ActionResult<List<Usuario>> ObtenerUsuarios()
        {
            var datos = _db.Usuarios
                .Select(e => new
                {
                    nombre = e.Nombre,
                    apellido = e.Apellido,
                }).ToList();
            return Ok(datos);
        }
        // Api para los servicion 
        [HttpGet("Servicios")]
        public ActionResult<List<Servicio>> Obeterservicios()
        {
            var servicios = _db.Servicios
                .Select(e => new
                {
                    Titulo = e.Titulo,
                    desccripcion = e.IdTipoServicioNavigation.Descripcion
                })
                .ToList();
            return Ok(servicios);

        }
        [HttpGet("Comentarios")]

        public ActionResult<List<Usuario>> ObtenerComentario()
        {
            var id = 1;
            var comentario = _db.Usuarios
                .Where(e=> e.IdUsuario == id )
                .Select(e=> new 
                {
                       Nombre = e.Nombre,
                       comentario = e.Comentarios.Select(e => e.Comentario1)
                })
                .ToList();
            return Ok(comentario);
        }
        [HttpGet("Transacciones")]

        public ActionResult transanccionesUsaurio()
        {
            var id = 1;
            var transacciones = _db.Transacciones
                .Include(c => c.IdUsuarioClienteNavigation)
                .Select(c => new
                {
                    nombre = c.IdUsuarioClienteNavigation.Nombre,
                    Monto = c.MontoTotal
                })
                .ToList();
 
            return Ok(transacciones);
        }
        [HttpGet("Presupuesto")]
        public ActionResult ObtenerPresupuestoAlto()
        {
            var presupuesto = _db.Servicios
                .OrderByDescending(e => e.Presupuesto)
                .Select(e => new
                {
                    Titulo = e.Titulo,
                    presupuesto = e.Presupuesto
                })
                .FirstOrDefault();
            return Ok(presupuesto);
        }

        [HttpGet("Pujas")]
        public ActionResult<List<Puja>> PujaMasAlta()
        {

            var pujas = _db.Pujas
                .GroupBy(p => p.IdServicio)
                .Select(e => new
                {
                    IdServicio= e.Key,
                    Nombre = e.Select(p => p.IdServicioNavigation.Titulo),
                    Monto = e.Max(p => p.Monto)
                }).ToList();
            return Ok(pujas);
        }

        [HttpGet("ServiciosOrdenados")]

        public ActionResult ObtenerServiciosOrdenados()
        {
            var servicios = _db.Servicios
                .OrderByDescending(p => p.FechaPublicacion)
                .Where(e => e.Estado  == "Publicado")
                .Select(e => new
                {
                    nombre = e.Titulo,
                    fecha = e.FechaPublicacion
                })
                
                .Take(5)
                .ToList();
            return Ok(servicios);
        }

        [HttpGet("ServiciosPujas")]

        public ActionResult ObtenerServiciosPuja()
        {
            var cantidad = 50;
            var serPuja = _db.Pujas
                .Include(e => e.IdServicioNavigation)
                .Where(c  => c.Monto >=  cantidad)
                .Select(e => new
                {

                    nombre = e.IdServicioNavigation.Titulo,
                    Monto = e.Monto
                }).ToList();
            return Ok(serPuja);
        }

        [HttpGet("TipoServicio")]
        public ActionResult obtenerUsuario()
        {
            var usuarios = from u in _db.Usuarios
                           join s in _db.Servicios on u.IdUsuario equals s.IdUsuario
                           join ts in _db.TiposServicios on s.IdTipoServicio equals ts.IdTipoServicio
                           select new
                           {
                               u.Nombre,
                               ts.Descripcion
                           };
            return Ok(usuarios);
        }
    }  
}
