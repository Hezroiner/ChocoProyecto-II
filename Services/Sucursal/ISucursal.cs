using Proyecto_II.Entities;

namespace Proyecto_II.Services
{
    public interface ISucursal
    {
        public List<Sucursal> GetAll();
        public Sucursal GetById(int id);
    }
}
