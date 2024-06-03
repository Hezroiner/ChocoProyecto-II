using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CitaDTO
    {

        public int CitaId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public int TipoCitaId { get; set; }
        public string TipoCitaNombre { get; set; } 
        public int SucursalId { get; set; }
        public string SucursalNombre { get; set; }
    }
}
