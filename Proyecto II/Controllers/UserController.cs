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

        //Get All
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _svUser.GetAll();
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
    

    //Post
    [HttpPost]
        public void Post([FromBody] User user)
        {
            _svUser.AddUser(user);
        }
    }
}
