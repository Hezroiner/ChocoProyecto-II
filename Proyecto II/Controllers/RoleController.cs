using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services;

namespace Proyecto_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private IRole _svRole;

        public RoleController(IRole svRole)
        {
            _svRole = svRole;
        }

        [HttpGet]
        public IEnumerable<Role> Get()
        {
            return _svRole.GetAll();
        }

        // Get By Id
        [HttpGet("{id}")]
        public Role Get(int id)
        {
            return _svRole.GetById(id);
        }
    }
}
