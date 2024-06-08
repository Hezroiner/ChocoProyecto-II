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
                        UserId = user.UserId,
                        Nombre = user.Nombre,
                        Email = user.Email,
                        Telefono = user.Telefono
                        
                    });
                }

                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ha ocurrido un error interno en el servidor.", details = ex.Message });
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
                    UserId = user.UserId,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    Telefono = user.Telefono
                    
                };

                return Ok(userDTO);  
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ha ocurrido un error interno en el servidor.", details = ex.Message });
            }
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterModel model)
        {
            try
            {
                _svUser.Register(model);
                return Ok("Usuario registrado exitosamente."); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ha ocurrido un error interno en el servidor.", details = ex.Message });
            }
        }


        [HttpPost("login")]
        public IActionResult Login(UserLoginModel model)
        {
            try
            {
                var token = _svUser.Login(model);
                return Content(token); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
