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
            if (_myContext.Citas.Any(c =>
                c.UserId == cita.UserId &&
                c.FechaHora.Date == cita.FechaHora.Date &&
                c.Status == "ACTIVA"))
            {
                throw new InvalidOperationException("No se puede crear otra cita para el mismo paciente en el mismo día.");
            }
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
            .Include(cita => cita.User)    // Incluye la relación con User
            .Include(cita => cita.TipoCita)    // Incluye la relación con TipoCita
            .Include(cita => cita.Sucursal)    // Incluye la relación con Sucursal
            .ToList();
        }

        public Cita GetById(int id)
        {
            var cita = _myContext.Citas
            .Include(cita => cita.User)
            .Include(cita => cita.TipoCita)
            .Include(cita => cita.Sucursal)
            .FirstOrDefault(cita => cita.Id == id);

            if (cita == null)
            {
                // Manejo de la situación cuando no se encuentra la entidad choco
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
                updateCita.Lugar = newCita.Lugar;
                updateCita.Status = newCita.Status;

                _myContext.Citas.Update(updateCita);
                _myContext.SaveChanges();
            }

            return updateCita;
        }

        public void CancelarCita(int id)
        {
            var cita = GetById(id);

            if (cita.FechaHora < DateTime.Now.AddHours(24))
            {
                throw new InvalidOperationException("Las citas se deben cancelar con mínimo 24 horas de antelación.");
            }
            cita.Status = "CANCELADA";
            _myContext.Citas.Update(cita);
            _myContext.SaveChanges();
        }
    }
}
