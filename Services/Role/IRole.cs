using Proyecto_II.Entities;

namespace Proyecto_II.Services
{
    public interface IRole
    {
        public List<Role> GetAll();
        public Role GetById(int id);
    }
}
