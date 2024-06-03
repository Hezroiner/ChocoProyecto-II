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

        [HttpPost]
        public ActionResult<CitaDTO> AddCita([FromBody] CitaDTO citaDTO)
        {
            if (citaDTO == null)
            {
                return BadRequest("CitaDTO cannot be null");
            }

            try
            {
                var addCita = _svCita.AddCita(citaDTO);
                return CreatedAtAction(nameof(AddCita), new { id = addCita.CitaId }, addCita);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Cita
        [HttpGet]
        public ActionResult<IEnumerable<CitaDTO>> Get()
        {
            try
            {
                var citas = _svCita.GetAll();
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
                if (cita == null)
                {
                    return NotFound("Cita not found.");
                }

                return Ok(cita);
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


        [HttpGet("user/{userId}")]
        public IActionResult GetCitasByUserId(int userId)
        {
            try
            {
                var citas = _svCita.GetCitaByUserId(userId);
                return Ok(citas);
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




        // PUT: api/Cita/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, CitaDTO citaDTO)
        {
            try
            {
                // Verificar si la cita existe
                var existingCita = _svCita.GetById(id);
                if (existingCita == null)
                {
                    return NotFound("Cita not found.");
                }

                // Actualizar los datos de la cita existente con los datos proporcionados en el DTO
                existingCita.FechaHora = citaDTO.FechaHora;
                existingCita.Status = citaDTO.Status;
                existingCita.UserId = citaDTO.UserId;
                existingCita.TipoCitaId = citaDTO.TipoCitaId;
                existingCita.SucursalId = citaDTO.SucursalId;

                // Llamar al método en el servicio para actualizar la cita
                _svCita.UpdateCita(existingCita);

                // Retornar la cita actualizada
                return Ok(existingCita);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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
