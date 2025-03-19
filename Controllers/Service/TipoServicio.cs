using AppTareas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppTareas.Controllers.Service
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicio : ControllerBase
    {

        private readonly ServiceContext _db;

        public TipoServicio(ServiceContext db)
        {
            _db = db;
        }

    }
}
