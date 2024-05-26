using Entitites;
using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services;

namespace Proyecto_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUser _svUser;
        public UserController(IUser svUser)
        {
            _svUser = svUser;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                var users = _svUser.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Error interno del servidor
            }
        }


        //GetById
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _svUser.GetById(id);
                return Ok(user);  // Retorna el usuario si es encontrado
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
    

<<<<<<< HEAD
    //Post
    [HttpPost]
        public void Post([FromBody] User user)
=======

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterModel model)
>>>>>>> Hezroiner
        {
            try
            {
                var token = _svUser.Register(model);
                if (token == null)
                {
                    return BadRequest("No se pudo registrar el usuario.");
                }
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Error en la solicitud del cliente
            }
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                _svUser.AddUser(user);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
