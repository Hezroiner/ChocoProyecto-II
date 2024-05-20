using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;
using System.Data.Entity;

namespace Proyecto_II.Services
{
    public class SvTipoCita : ITipoCita
    {
        private MyContext _myContext = default!;

        public SvTipoCita()
        {
            _myContext = new MyContext();
        }
        public List<TipoCita> GetAll()
        {
            return _myContext.TiposCita
                .Include(tipocita => tipocita.Citas)
                .ToList();
        }

        public TipoCita GetById(int id)
        {
            return _myContext.TiposCita
                 .Include(tipocita => tipocita.Citas)
                .FirstOrDefault(t => t.Id == id);
        }
    }
}
