namespace Proyecto_II.Entities
{
    public class Sucursal
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Lugar { get; set; }
        public string Status { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int TipoCitaId { get; set; }
        public TipoCita TipoCita { get; set; }
        public int SucursalId { get; set; }
        public Sucursal Sucursales { get; set; }

    }
}
