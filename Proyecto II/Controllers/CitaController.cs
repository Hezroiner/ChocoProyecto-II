using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services;
using Services.DTO;
using System;
using System.Collections.Generic;

namespace Proyecto_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : Controller
    {
        private ICita _svCita;

        public CitaController(ICita svCita)
        {
            _svCita = svCita;
        }


        // GET: api/Cita
        [HttpGet]
        public ActionResult<IEnumerable<Cita>> Get()
        {
            try
            {
                var citas = _svCita.GetAll(); // Suponiendo que GetAll() devuelve una lista de objetos Cita.
                return Ok(citas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }


        // GET: api/Cita/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var cita = _svCita.GetById(id);
                var citaDTO = new CitaDTO
                {
                    CitaId = cita.CitaId,
                    FechaHora = cita.FechaHora,
                    Status = cita.Status,
                    UserId = cita.UserId,
                    TipoCitaId = cita.TipoCitaId,
                    SucursalId = cita.SucursalId
                };

                return Ok(citaDTO);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // POST: api/Cita
        [HttpPost]
        public IActionResult Post(CitaDTO citaDTO)
        {
            try
            {
                var cita = new Cita
                {
                    FechaHora = citaDTO.FechaHora,
                    Status = citaDTO.Status,
                    UserId = citaDTO.UserId,
                    TipoCitaId = citaDTO.TipoCitaId,
                    SucursalId = citaDTO.SucursalId
                };

                _svCita.AddCita(cita);

                return Ok(cita);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

           
        // Update Cita
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cita cita)
        {
            try
            {
                _svCita.Update(id, cita);
                return Ok("Cita actualizada correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Delete Cita
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _svCita.Delete(id);
                return Ok("Cita eliminada correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Cancelar Cita
        [HttpPatch("cancelar/{id}")]
        public IActionResult CancelarCita(int id)
        {
            try
            {
                _svCita.CancelarCita(id);
                return Ok("Cita cancelada con éxito.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Manejo general de excepciones
                return StatusCode(500, new { message = "Ha ocurrido un error interno en el servidor.", details = ex.Message });
            }
        }
    }
}
