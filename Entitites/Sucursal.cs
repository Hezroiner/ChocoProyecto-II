namespace Proyecto_II.Entities
{
    public class Sucursal
    {
        public int Id { get; set; }
        public string NombreSucursal { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public List<Cita> Citas { get; set; }

    }
}
