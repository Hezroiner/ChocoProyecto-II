



using Microsoft.EntityFrameworkCore;
using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;

namespace Proyecto_II.Services
{
    public class SvCita : ICita
    {
        private  MyContext _myContext = default!;

        public SvCita() 
        {
            _myContext = new MyContext();
        }

        public Cita AddCita(Cita cita)
        {
            _myContext.Citas.Add(cita);
            _myContext.SaveChanges();

            return cita;
        }

        public void Delete(int id)
        {
           Cita deleteCita = _myContext.Citas.Find(id);

            if (deleteCita is not null)
            {
                _myContext.Citas.Remove(deleteCita);
                _myContext.SaveChanges();
            }
        }

        public List<Cita> GetAll()
        {
            return _myContext.Citas
            .Include(cita => cita.Paciente)    // Incluye la relación con Paciente
            .Include(cita => cita.TipoCita)    // Incluye la relación con TipoCita
            .Include(cita => cita.Sucursal)    // Incluye la relación con Sucursal
            .ToList();
        }

        public Cita GetById(int id)
        {
            var cita = _myContext.Citas
            .Include(cita => cita.Paciente)
            .Include(cita => cita.TipoCita)
            .Include(cita => cita.Sucursal)
            .FirstOrDefault(cita => cita.Id == id);

            if (cita == null)
            {
                // Manejo de la situación cuando no se encuentra la entidad
                throw new KeyNotFoundException("Cita not found");
            }

            return cita;
        }

        public Cita Update(int id, Cita newCita)
        {
            Cita updateCita = _myContext.Citas.Find(id);

            if (updateCita is not null) 
            {
                updateCita.FechaHora = newCita.FechaHora;
                updateCita.FechaHora = newCita.FechaHora;
                updateCita.Status = newCita.Status;

                _myContext.Citas.Update(updateCita);
                _myContext.SaveChanges();
            }

            return updateCita;
        }
    }
}
