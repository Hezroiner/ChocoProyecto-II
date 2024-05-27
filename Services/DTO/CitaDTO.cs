using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CitaDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public int TipoCitaId { get; set; }
        public int SucursalId { get; set; }
    }
}
