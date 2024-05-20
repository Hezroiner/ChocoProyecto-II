using Microsoft.EntityFrameworkCore;
using Proyecto_II.Entities;
using Services.MyDbContext;

namespace Proyecto_II.Services
{
    public class SvRole : IRole
    {
        private  MyContext _myContext = default!;

        public SvRole()
        {
            _myContext = new MyContext();
        }

        public List<Role> GetAll()
        {
            return _myContext.Roles
                 .Include(role => role.Users) //Relacion de role y users
                .ToList();
        }

        public Role GetById(int id)
        {
            var role = _myContext.Roles
                .Include(role => role.Users) //Relacion de role y users
                .FirstOrDefault(user => user.RoleId == id);

            if (role == null)
            {
                // Manejo de la situación cuando no se encuentra la entidad choco
                throw new KeyNotFoundException("Role not found");
            }

            return role;
        }

    }
}
