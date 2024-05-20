using Proyecto_II.Entities;

namespace Services
{
    public interface ISucursal
    {
        public List<Sucursal> GetAll();
        public Sucursal GetById(int id);
    }
}
