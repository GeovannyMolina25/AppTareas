using AppTareas.Models;
using AppTareas.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppTareas.Controllers.dashboard
{
    public class dashboardController : Controller
    {
        private readonly ServiceContext _db;
        private readonly TransactionServices _service;
        
        public dashboardController(ServiceContext db, TransactionServices service)
        {
            _db = db;
            _service = service;
        }
        [Authorize] 
        public IActionResult Index()
        {
            var datos = _service.ObtenerDatos();
            return View(datos);
        }
    }

}
