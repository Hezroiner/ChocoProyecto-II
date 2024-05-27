using Entitites;
using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services;
using Services.DTO;

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
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            try
            {
                var users = _svUser.GetAll();
                var userDTOs = new List<UserDTO>();

                foreach (var user in users)
                {
                    userDTOs.Add(new UserDTO
                    {
                        Id = user.Id,
                        Nombre = user.Nombre,
                        Email = user.Email,
                        Telefono = user.Telefono
                        // Puedes mapear otras propiedades aquí si es necesario
                    });
                }

                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // GetById
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _svUser.GetById(id);

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    Telefono = user.Telefono
                    // Puedes mapear otras propiedades aquí si es necesario
                };

                return Ok(userDTO);  // Retorna el usuario si es encontrado
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Retorna un 404 Not Found si no se encuentra el usuario
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterModel model)
        {
            try
            {
                var token = _svUser.Register(model);
                return Content(token, "text/plain"); // Ensure it returns as plain text
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginModel model)
        {
            try
            {
                var token = _svUser.Login(model);
                return Content(token); // Ensure it returns as plain text
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
