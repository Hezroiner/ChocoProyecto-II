using Microsoft.EntityFrameworkCore;
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
            return _myContext.Sucursales
         .Include(sucursal => sucursal.Citas)    // Incluye la relación con Citas
         .ToList();
        }

        public Sucursal GetById(int id)
        {
            var sucursal = _myContext.Sucursales
                .Include(sucursal => sucursal.Citas)  // Incluye la relación con Citas
                .FirstOrDefault(sucursal => sucursal.Id == id);

            if (sucursal == null)
            {
                throw new KeyNotFoundException("Sucursal not found");
            }

            return sucursal;
        }
    }
}
