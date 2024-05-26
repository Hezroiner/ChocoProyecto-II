namespace Proyecto_II.Entities
{
    public class Cita
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Lugar { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int TipoCitaId { get; set; }
        public TipoCita TipoCita { get; set; }
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
    }

}

