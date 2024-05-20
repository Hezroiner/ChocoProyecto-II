using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services.MyDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_II.Services
{
    public class SvRole : IRole
    {
        private  MyContext _myContext = default!;

        public SvRole(MyContext context)
        {
            _myContext = context;
        }

        public List<Role> GetAll()
        {
            return _myContext.Roles.ToList();
        }

        public Role GetById(int id)
        {
            var role = _myContext.Roles.FirstOrDefault(user => user.RoleId == id);

            if (role == null)
            {
                // Manejo de la situación cuando no se encuentra la entidad choco
                throw new KeyNotFoundException("Sucursal not found");
            }

            return role;
        }

    }
}
