using Microsoft.EntityFrameworkCore;
using Proyecto_II.Entities;
using Services.MyDbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_II.Services
{
    public class SvRole : IRole
    {
        private readonly MyContext _myContext;

        public SvRole(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<Role> GetAll()
        {
            return _myContext.Roles
                 .Include(role => role.Users)
                .ToList();
        }

        public Role GetById(int id)
        {
            var role = _myContext.Roles
                .Include(role => role.Users)
                .FirstOrDefault(role => role.RoleId == id);

            if (role == null)
            {
                throw new KeyNotFoundException("Role no encontrado");
            }

            return role;
        }
    }
}
