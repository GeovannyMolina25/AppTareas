using AppTareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppTareas.Controllers.Service
{
    public class ServiceController : Controller
    {
        private readonly ServiceContext _db;
        
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
