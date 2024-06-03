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
                .FirstOrDefault(user => user.UserId == id);

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

        public void Register(UserRegisterModel model)
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
        }


        public string Login(UserLoginModel model)
        {
            var user = _myContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == model.Email);

            if (user == null || user.Password != model.Password)
            {
                throw new Exception("Invalid credentials.");
            }

            var token = GenerateJwtToken(user);
            return token;
        }


        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);
            var issuedAt = DateTime.UtcNow;
            var expires = issuedAt.AddDays(7);

          
            var claims = new List<Claim>
    {
        new Claim("Id", user.UserId.ToString()), 
        new Claim("Nombre", user.Nombre),        
        new Claim("Email", user.Email),          
        new Claim("Telefono", user.Telefono),
        new Claim("RoleId", user.Role.RoleId.ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(issuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                NotBefore = issuedAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
