using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;

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
            return _myContext.TiposCita.ToList();
        }

        public TipoCita GetById(int id)
        {
            return _myContext.TiposCita.FirstOrDefault(t => t.Id == id);
        }
    }
}
