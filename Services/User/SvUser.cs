
using Microsoft.EntityFrameworkCore;
using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;

namespace Proyecto_II.Services
{
    public class SvUser : IUser
    {
        private MyContext _myContext = default!;

        public SvUser()
        {
            _myContext = new MyContext();
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
                .Include(user => user.Citas)  // Relación de User y Citas
                .Include(user => user.Role)   // Relación de User y Role
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
    }
}
