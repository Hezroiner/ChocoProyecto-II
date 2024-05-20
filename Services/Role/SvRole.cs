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
        private readonly MyContext _myContext;

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
            return _myContext.Roles.FirstOrDefault(r => r.RoleId == id);
        }

        public void Delete(int id)
        {
            Role deleteRole = _myContext.Roles.Find(id);

            if (deleteRole is not null)
            {
                _myContext.Roles.Remove(deleteRole);
                _myContext.SaveChanges();
            }
        }
    }
}
