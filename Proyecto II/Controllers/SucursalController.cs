using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services;

namespace Proyecto_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private ISucursal _svSucursal;

        public SucursalController(ISucursal svSucursal)
        {
            _svSucursal = svSucursal;
        }

        // Get All
        [HttpGet]
        public IEnumerable<Sucursal> Get()
        {
            return _svSucursal.GetAll();
        }

        // Get By Id
        [HttpGet("{id}")]
        public Sucursal Get(int id)
        {
            return _svSucursal.GetById(id);
        }
    }
}
