using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Services;
using Services;
using Services.DTO;
using System.Collections.Generic;

namespace Proyecto_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCitaController : ControllerBase
    {
        private readonly ITipoCita _svTipoCita;

        public TipoCitaController(ITipoCita svTipoCita)
        {
            _svTipoCita = svTipoCita;
        }

        // Get All
        [HttpGet]
        public IEnumerable<TipoCitaDTO> Get()
        {
            var tiposCita = _svTipoCita.GetAll();
            var tipoCitaDTOs = new List<TipoCitaDTO>();

            foreach (var tipoCita in tiposCita)
            {
                tipoCitaDTOs.Add(new TipoCitaDTO
                {
                    Id = tipoCita.Id,
                    Nombre = tipoCita.Nombre
                    // Puedes mapear otras propiedades aquí si es necesario
                });
            }

            return tipoCitaDTOs;
        }

        // Get By Id
        [HttpGet("{id}")]
        public TipoCitaDTO Get(int id)
        {
            var tipoCita = _svTipoCita.GetById(id);

            return new TipoCitaDTO
            {
                Id = tipoCita.Id,
                Nombre = tipoCita.Nombre
                // Puedes mapear otras propiedades aquí si es necesario
            };
        }
    }
}
