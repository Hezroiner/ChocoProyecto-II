namespace Proyecto_II.Entities
{
    public class Sucursal
    {
        public int SucursalId { get; set; }
        public string Nombre { get; set; }
        public List<Cita> Citas { get; set; }

    }
}
