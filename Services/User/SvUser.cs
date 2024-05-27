using Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyecto_II.Services
{
    public class SvUser : IUser
    {
        private readonly MyContext _myContext;
        private readonly string _jwtKey;

        public SvUser(MyContext myContext, IConfiguration configuration)
        {
            _myContext = myContext;
            _jwtKey = configuration["Jwt:Key"];
        }

        public List<User> GetAll()
        {
            return _myContext.Users
                 .Include(user => user.Citas)
                 .Include(user => user.Role)
                .ToList();
        }

        public User GetById(int id)
        {
            var user = _myContext.Users
                .Include(user => user.Citas)
                .Include(user => user.Role)
                .FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User not found with ID {id}");
            }

            return user;
        }

        public User AddUser(User user)
        {
            _myContext.Users.Add(user);
            _myContext.SaveChanges();

            return user;
        }

        public string Register(UserRegisterModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Nombre) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentException("Los campos requeridos no pueden estar vacíos.");
            }

            if (_myContext.Users.Any(u => u.Email == model.Email))
            {
                throw new Exception("El correo electrónico ya está en uso.");
            }

            var user = new User
            {
                Nombre = model.Nombre,
                Email = model.Email,
                Telefono = model.Telefono,
                Password = model.Password,
                RoleId = 2 // Asignar un rol de usuario por defecto
            };

            _myContext.Users.Add(user);
            _myContext.SaveChanges();

            return GenerateJwtToken(user);
        }

        public string Login(UserLoginModel model)
        {
            var user = _myContext.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null || user.Password != model.Password)
            {
                throw new Exception("Invalid credentials.");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Nombre),
        new Claim(ClaimTypes.Email, user.Email),
            };

            var tokenDescriptor = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );

            return tokenHandler.WriteToken(tokenDescriptor);
        }
    }
}
