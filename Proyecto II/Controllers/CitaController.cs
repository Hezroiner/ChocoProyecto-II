using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Services;

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

        //Get All
        [HttpGet]
        public IEnumerable<Cita> Get()
        {
            return _svCita.GetAll();
        }

        //GetById
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var cita = _svCita.GetById(id);
                return Ok(cita);  // Retorna la cita si es encontrada
            }
            catch (KeyNotFoundException ex)
            {
                // Maneja la excepción y retorna un 404 Not Found
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Maneja cualquier otra excepción y retorna un 500 Internal Server Error
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post(Cita cita)
        {
            try
            {
                _svCita.AddCita(cita);
                return Ok(cita); // Retorna la cita agregada si se realiza con éxito
            }
            catch (InvalidOperationException ex)
            {
                // Maneja la excepción específica que indica que ya existe una cita para el mismo paciente en el mismo día
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Maneja otras excepciones de manera genérica
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }


        //Put
        [HttpPut("{id}")]
            public void Put(int id, [FromBody] Cita cita)
            {
                _svCita.Update(id, new Cita
                {
                    FechaHora = cita.FechaHora,
                    Lugar = cita.Lugar,
                    Status = cita.Status
                });
            }

            //Delete
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
                _svCita.Delete(id);
            }

            //Cancelar Cita
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
                    return StatusCode(500, "Ha ocurrido un error interno en el servidor.");
                }
            }
        }
    }
