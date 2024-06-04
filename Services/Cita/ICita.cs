using Proyecto_II.Entities;
using Services.DTO;

namespace Services

{
    public interface ICita
    {
        public CitaDTO AddCita(CitaPostDTO citaPostDTO);
        public IEnumerable<CitaDTO> GetAll();
        public CitaDTO GetById(int id);
        public List<CitaDTO> GetCitaByUserId(int userId);
        public CitaDTO UpdateCita(int id, CitaPostDTO citaPostDTO);
        public void Delete(int id);
        public void CancelarCita(int id);
    }
}

