using AppTareas.Models;
using AppTareas.Models.View_models;
using AppTareas.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Claims;

namespace AppTareas.Controllers.Service
{
    public class ServiceController : Controller
    {
        private readonly ServiceContext _db;
        private readonly ServiceService _service;

        public ServiceController(ServiceContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var usuario = User.Claims.FirstOrDefault(e => e.Type == "IdUsuario")?.Value;
            var idUsuario = int.Parse(usuario);
            var datos = _db.Servicios
                .Where(e => e.IdUsuario == idUsuario)
                .ToList();

            return View(datos);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var TipoServicios = _db.TiposServicios
                .Select(e => new TiposServiciosViewModel
                {
                    IdTipoServicio = e.IdTipoServicio,
                    NombreTipo = e.NombreTipo,
                })
                .ToList();
            ViewBag.TiposServicios = TipoServicios;

            return View(TipoServicios);
        }
        [HttpPost]
        public IActionResult Add(TiposServiciosViewModel modelServicio)
        {
            try
            {
                var usuario = User.Claims.FirstOrDefault(e => e.Type == "IdUsuario")?.Value;
                var idUsuario = int.Parse(usuario);
                var servicio = new Servicio
                {
                    IdUsuario = idUsuario,
                    IdTipoServicio = modelServicio.IdTipoServicio,
                    Titulo = modelServicio.Titulo,
                    Descripcion = modelServicio.Descripcion,
                    Presupuesto = modelServicio.Presupuesto,
                    HorasEstimadas = modelServicio.HorasEstimadas,
                    FechaPublicacion = DateTime.Now,
                    Estado = "Publicado"
                };
                _db.Add(servicio);
                _db.SaveChanges();
                TempData["success"] = $"El servicio {modelServicio.Titulo} ha sido ingresado correctamente";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
                TempData["error"] = ex.Message;
                return RedirectToAction("Add");
            }
        }
        [HttpPost]
        public IActionResult details(int id)
        {
            var datos = _db.TiposServicios.FirstOrDefault(e => e.IdTipoServicio == id);


            return View(datos);
        }
        public IActionResult delete(int id)
        {
            var datos = _db.TiposServicios.FirstOrDefault(e => e.IdTipoServicio == id);
            return View(datos);
        }
    }
}
