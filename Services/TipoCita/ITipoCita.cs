using Proyecto_II.Entities;

namespace Services
{
    public interface ITipoCita
    {
        public List<TipoCita> GetAll();
        public TipoCita GetById(int id);
    }
}
