using AppTareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppTareas.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly ServiceContext _db;

        public LoginController(ServiceContext db)
        {
            _db = db;
        }
        [HttpPost]
        [Route("/Login")]

        public IActionResult Login(string email, string password)
        {
            var usuario = _db.Usuarios
                .FirstOrDefault(e => e.Email == email && e.Contrasena == password);
            if(usuario != null)
            {
                TempData["mesage"] = $"Bienvenido usuario {usuario.Nombre} {usuario.Apellido}";
                return RedirectToAction("Index", "dashboard");
            }
            else
            {
                TempData["error"] = $"Usurio con el email {email} no encontrado";
                return RedirectToAction("Index");
            } 
           
        }
        public IActionResult Index()
        {
            return View();
        }
    }
    

}
