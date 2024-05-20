using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;

namespace Proyecto_II.Services
{
    public class SvSucursal : ISucursal
    {
        private MyContext _myContext = default!;

        public SvSucursal()
        {
            _myContext = new MyContext();
        }

        public List<Sucursal> GetAll()
        {
            return _myContext.Sucursales.ToList();
        }

        public Sucursal GetById(int id)
        {
            var sucursal = _myContext.Sucursales.FirstOrDefault(cita => cita.Id == id);

            if (sucursal == null)
            {
                // Manejo de la situación cuando no se encuentra la entidad choco
                throw new KeyNotFoundException("Sucursal not found");
            }

            return sucursal;
        }
    }
}
