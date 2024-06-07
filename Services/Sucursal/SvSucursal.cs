using Microsoft.EntityFrameworkCore;
using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_II.Services
{
    public class SvSucursal : ISucursal
    {
        private readonly MyContext _myContext;

        public SvSucursal(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<Sucursal> GetAll()
        {
            return _myContext.Sucursales
                .Include(sucursal => sucursal.Citas)
                .ToList();
        }

        public Sucursal GetById(int id)
        {
            var sucursal = _myContext.Sucursales
                .Include(sucursal => sucursal.Citas)
                .FirstOrDefault(sucursal => sucursal.SucursalId == id);

            if (sucursal == null)
            {
                throw new KeyNotFoundException("Sucursal no encontrado");
            }

            return sucursal;
        }
    }
}
