using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Data;

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

        //Post
        [HttpPost]
        [Authorize(Policy = "USER")]
        public async Task<ActionResult<CitaDTO>>PostCita(CitaPostDTO citaPostDTO)
        {
            try
            {
                var citaDTO = _svCita.AddCita(citaPostDTO);
                return CreatedAtAction(nameof(Get), new { id = citaDTO.CitaId }, citaDTO);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
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
                return StatusCode(500, new { message = "Ha ocurrido un error interno en el servidor.", details = ex.Message });
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
                return StatusCode(500, new { message = "Ha ocurrido un error interno en el servidor.", details = ex.Message });
            }
        }

        //GetbyIdUser
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
                return StatusCode(500, new { message = "Ha ocurrido un error interno en el servidor.", details = ex.Message });
            }
        }



        //Put
        [HttpPut("{id}")]
        [Authorize(Policy = "USER")]
        public ActionResult<CitaDTO> PutCita(int id, CitaPostDTO citaPostDTO)
        {
            try
            {
                var citaDTO = _svCita.UpdateCita(id, citaPostDTO);
                return Ok(citaDTO);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpDelete("{id}")]
        [Authorize(Policy = "ADMIN")]
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
                return StatusCode(500, new { message = "Ha ocurrido un error interno en el servidor.", details = ex.Message });
            }
        }
        //Get Fecha
        [HttpGet("fecha")]
        public ActionResult<List<CitaDTO>> GetByFechaCita([FromQuery] DateTime fecha)
        {
            try
            {
                var citas = _svCita.GetByFechaCita(fecha);
                return Ok(citas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Patch
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
                
                return StatusCode(500, new { message = "Ha ocurrido un error interno en el servidor.", details = ex.Message });
            }
        }
    }
}
