using Proyecto_II.Entities;
using Services.DTO;

namespace Services

{
    public interface ICita
    {
        public CitaDTO AddCita(CitaDTO citaDTO);
        public IEnumerable<CitaDTO> GetAll();
        public CitaDTO GetById(int id);
        public void UpdateCita(CitaDTO citaDTO);
        public void Delete(int id);
        public void CancelarCita(int id);
    }
}

