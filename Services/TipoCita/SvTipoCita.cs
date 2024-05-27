using Microsoft.EntityFrameworkCore;
using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;


namespace Proyecto_II.Services
{
    public class SvTipoCita : ITipoCita
    {
        private readonly MyContext _myContext;

        public SvTipoCita(MyContext myContext)
        {
            _myContext = myContext;
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
                .FirstOrDefault(t => t.TipoCitaId == id);
        }
    }
}
