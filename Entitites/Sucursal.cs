namespace Proyecto_II.Entities
{
    public class Sucursal
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Lugar { get; set; }
        public string Status { get; set; }
<<<<<<< HEAD
=======


        public int UserId { get; set; }
        public User User { get; set; }
>>>>>>> Hezroiner
        public int TipoCitaId { get; set; }
        public TipoCita TipoCita { get; set; }
        public int SucursalId { get; set; }
        public Sucursal Sucursales { get; set; }
        public List<Cita> Citas { get; set; }

    }
}
