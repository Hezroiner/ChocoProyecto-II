using Entitites;
using Microsoft.IdentityModel.Tokens;
using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Proyecto_II.Services
{
    public class SvUser : IUser
    {
        private readonly MyContext _myContext;

        public SvUser(MyContext myContext)
        {
            _myContext = myContext;
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
                throw new KeyNotFoundException("User not found with ID: " + id);
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
            // Verificar que el modelo no sea nulo y que los campos requeridos no sean nulos o vacíos
            if (model == null || string.IsNullOrEmpty(model.Nombre) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentException("Los campos requeridos no pueden estar vacíos.");
            }

            // Verificar si el correo electrónico ya está en uso
            if (_myContext.Users.Any(u => u.Email == model.Email))
            {
                throw new Exception("El correo electrónico ya está en uso.");
            }

            // Crear el usuario solo si pasa las verificaciones anteriores
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

            // Generar y devolver el token JWT solo si el usuario se agrega correctamente
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
            //if (user == null || user.Role == null)
            //{
            //    throw new ArgumentNullException("El usuario o el rol es nulo.");
            //}

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your_secret_key_here"); // Reemplaza con tu clave secreta real
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nombre),
            new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
