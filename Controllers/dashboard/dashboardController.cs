using Microsoft.AspNetCore.Mvc;

namespace AppTareas.Controllers.dashboard
{
    public class dashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
