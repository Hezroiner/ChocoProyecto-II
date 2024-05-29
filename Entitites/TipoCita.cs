namespace Proyecto_II.Entities
{
    public class TipoCita
    {
        public int TipoCitaId { get; set; }
        public string Nombre { get; set; }
        public List<Cita> Citas { get; set; }
    }
}