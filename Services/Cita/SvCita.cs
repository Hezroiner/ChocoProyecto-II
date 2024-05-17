

using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;

namespace Proyecto_II.Services
{
    public class SvICita : ICita
    {
        private  MyContext _myContext = default!;

        public SvICita() 
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
            return _myContext.Citas.Include(x => x.Books).ToList();
        }

        public Cita GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Cita cita)
        {
            throw new NotImplementedException();
        }
    }
}
