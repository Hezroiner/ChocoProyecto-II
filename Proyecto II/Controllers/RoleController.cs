using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services;
using Services.DTO;

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

        // Get All
        [HttpGet]
        public IEnumerable<RoleDTO> Get()
        {
            var roles = _svRole.GetAll();
            var roleDTOs = new List<RoleDTO>();

            foreach (var role in roles)
            {
                roleDTOs.Add(new RoleDTO
                {
                    RoleId = role.RoleId,
                    Nombre = role.Nombre,

                });
            }

            return roleDTOs;
        }

        // Get By Id
        [HttpGet("{id}")]
        public RoleDTO Get(int id)
        {
            var role = _svRole.GetById(id);

            return new RoleDTO
            {
                RoleId = role.RoleId,
                Nombre = role.Nombre
            };
        }

    }
}
