namespace Proyecto_II.Entities
{
    public class Sucursal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public List<Cita> Citas { get; set; }

    }
}
