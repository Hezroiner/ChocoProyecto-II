using Microsoft.AspNetCore.Mvc;

using Proyecto_II.Services;
using Services;
using Services.DTO;
using System.Collections.Generic;

namespace Proyecto_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly ISucursal _svSucursal;

        public SucursalController(ISucursal svSucursal)
        {
            _svSucursal = svSucursal;
        }

        // Get All
        [HttpGet]
        public IEnumerable<SucursalDTO> Get()
        {
            var sucursales = _svSucursal.GetAll();
            var sucursalDTOs = new List<SucursalDTO>();

            foreach (var sucursal in sucursales)
            {
                sucursalDTOs.Add(new SucursalDTO
                {
                    SucursalId = sucursal.SucursalId,
                    Nombre = sucursal.Nombre
                    
                });
            }

            return sucursalDTOs;
        }

        // Get By Id
        [HttpGet("{id}")]
        public SucursalDTO Get(int id)
        {
            var sucursal = _svSucursal.GetById(id);

            return new SucursalDTO
            {
                SucursalId = sucursal.SucursalId,
                Nombre = sucursal.Nombre
                
            };
        }
    }
}
