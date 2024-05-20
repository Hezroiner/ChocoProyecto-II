using Microsoft.EntityFrameworkCore;
using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;

namespace Proyecto_II.Services
{
    public class SvUser : IUser
    {
        private MyContext _myContext = default!;

        public SvUser(MyContext context)
        {
            _myContext = context;
        }

        public List<User> GetAll()
        {
            return _myContext.Users
                 .Include(user => user.Citas) //Relacion de user y citas
                 .Include(user => user.Role) //Relacion de user y role
                .ToList();
        }

        public User GetById(int id)
        {
            var user = _myContext.Users
            .Include(user => user.Citas) //Relacion de user y citas
            .Include(user => user.Role) //Relacion de user y role
            .FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                // Manejo de la situación cuando no se encuentra la entidad choco
                throw new KeyNotFoundException("User not found");
            }

            return user;
        }

        public User AddUser(User user)
        {
            _myContext.Users.Add(user);
            _myContext.SaveChanges();

            return user;
        }
    }
}
