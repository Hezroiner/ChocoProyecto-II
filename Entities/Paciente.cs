namespace Proyecto_II.Entities
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public List<Cita> Citas { get; set; }
    }
}
