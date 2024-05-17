namespace Proyecto_II.Entities
{
    public class TipoCita
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Cita> Citas { get; set; }
    }
}