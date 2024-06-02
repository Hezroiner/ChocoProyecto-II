using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CitaPostDTO
    {
        public int CitaId { get; set; }
        public DateTime FechaHora { get; set; }
        public int UserId { get; set; }
        public int TipoCitaId { get; set; }
        public int SucursalId { get; set; }
    }
}
