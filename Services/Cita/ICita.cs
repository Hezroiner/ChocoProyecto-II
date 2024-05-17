using Proyecto_II.Entities;

namespace Services

{
    public interface ICita
    {
        public Cita Add(Cita cita);
        public List<Cita> GetAll();
        public Cita GetById(int id);

        public void Update(int id, Cita cita);
        public void Delete(int id);
    }
}
}
