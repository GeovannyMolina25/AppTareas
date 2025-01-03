using AppTareas.Models;
using AppTareas.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace AppTareas.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly ServiceContext _db;
        private readonly LoginServicio _encryp;

        
        public IActionResult Index()
        {
            ViewData["HideNavbar"] = true;
            return View();
        }
        public LoginController(ServiceContext db, LoginServicio encryp)
        {
            _db = db;
            _encryp = encryp;
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = _db.Usuarios
                .FirstOrDefault(e => e.Email == email);

            if (usuario != null)
            {
                if (_encryp.VerifyPassword(usuario.Contrasena, password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim(ClaimTypes.Surname, usuario.Apellido),
                        new Claim("IdUsuario", usuario.IdUsuario.ToString()) 
                    };

                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData["error"] = "Contraseña incorrecta";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["error"] = "Usuario no encontrado";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Logout()
        {
            // Cerrar sesión
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }
        // Comprobacion de los usuarios
        [HttpPost]
        [Route("/Register")]
        public IActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool Valemail = _db.Usuarios
                        .Any(e => e.Email == usuario.Email);
                    if (Valemail)
                    {
                        throw new Exception($"El usuario {usuario.Email} ya tiene cuenta");
                    }
                    var passwordEncrip = _encryp.EncryptPassword(usuario.Contrasena);
                    usuario.Contrasena = passwordEncrip;

                    _db.Usuarios.Add(usuario);
                    _db.SaveChanges();

                    TempData["success"] = $"El usuario {usuario.Nombre} {usuario.Apellido}, ha sido añadido correctamente";
                    return RedirectToAction("Index", "Login");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
            }
            TempData["error"] = "Error al ingresar datos de usuario";
            return View();
        }
        
    }
}
