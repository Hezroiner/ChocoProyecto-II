using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;
using System.Net.Mail;
using System.Net;
using Services.DTO;

namespace Proyecto_II.Services
{
    public class SvCita : ICita
    {
        private readonly MyContext _myContext;
        private readonly IConfiguration _configuration;

        public SvCita(MyContext myContext, IConfiguration configuration)
        {
            _myContext = myContext;
            _configuration = configuration;
        }

        public Cita AddCita(Cita cita)
        {
            // Verificar si ya existe una cita activa para el mismo paciente en el mismo día
            if (_myContext.Citas.Any(c =>
                c.UserId == cita.UserId &&
                c.FechaHora.Date == cita.FechaHora.Date &&
                c.Status == "ACTIVA"))
            {
                throw new InvalidOperationException("No se puede crear otra cita para el mismo paciente en el mismo día.");
            }

            // Agregar la nueva cita a la base de datos
            _myContext.Citas.Add(cita);
            _myContext.SaveChanges();

            // Llamar a la función para enviar el correo electrónico
            SendEmail(cita);

            return cita;
        }


        private void SendEmail(Cita cita)
        {
            var user = _myContext.Users.FirstOrDefault(u => u.UserId == cita.UserId);
            if (user == null)
            {
                // Manejar el caso cuando el usuario no está encontrado
                return;
            }

            // Cargar el tipo de cita y la sucursal desde la base de datos
            var tipoCita = _myContext.TiposCita.FirstOrDefault(tc => tc.TipoCitaId == cita.TipoCitaId);
            var sucursal = _myContext.Sucursales.FirstOrDefault(s => s.SucursalId == cita.SucursalId);

            var emailFrom = _configuration["EmailSettings:From"];
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
            var smtpUsername = _configuration["EmailSettings:Username"];
            var smtpPassword = _configuration["EmailSettings:Password"];
            var emailTo = user.Email;
            var subject = "Nueva cita creada";

            // Obtener el nombre del tipo de cita y la sucursal
            var tipoCitaNombre = tipoCita != null ? tipoCita.Nombre : "No especificado";
            var sucursalNombre = sucursal != null ? sucursal.Nombre : "No especificado";

            var body = $"Se ha creado una nueva cita para usted. Detalles: Fecha: {cita.FechaHora}, Tipo: {tipoCitaNombre}, Sucursal: {sucursalNombre}.";

            using (var message = new MailMessage(emailFrom, emailTo, subject, body))
            {
                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    client.Send(message);
                }
            }
        }

        public void Delete(int id)
        {
            Cita deleteCita = _myContext.Citas.Find(id);

            if (deleteCita is not null)
            {
                _myContext.Citas.Remove(deleteCita);
                _myContext.SaveChanges();
            }
        }

        public IEnumerable<CitaDTO> GetAll()
        {
            return _myContext.Citas.Select(c => new CitaDTO
            {
                CitaId = c.CitaId,
                FechaHora = c.FechaHora,
                Status = c.Status,
                UserId = c.UserId,
                UserName = c.User.Nombre, // Assuming 'Nombre' is a property in 'User'
                TipoCitaId = c.TipoCitaId,
                TipoCitaNombre = c.TipoCita.Nombre, // Assuming 'Nombre' is a property in 'TipoCita'
                SucursalId = c.SucursalId,
                SucursalNombre = c.Sucursal.Nombre // Assuming 'Nombre' is a property in 'Sucursal'
            }).ToList();
        }

        public CitaDTO GetById(int id)
        {
            var cita = _myContext.Citas.FirstOrDefault(c => c.CitaId == id);
            if (cita == null) return null;

            return new CitaDTO
            {
                CitaId = cita.CitaId,
                FechaHora = cita.FechaHora,
                Status = cita.Status,
                UserId = cita.UserId,
                UserName = cita.User.Nombre,
                TipoCitaId = cita.TipoCitaId,
                TipoCitaNombre = cita.TipoCita.Nombre,
                SucursalId = cita.SucursalId,
                SucursalNombre = cita.Sucursal.Nombre
            };
        }

        public void UpdateCita(CitaDTO citaDTO)
        {
            var cita = _myContext.Citas.FirstOrDefault(c => c.CitaId == citaDTO.CitaId);
            if (cita == null)
            {
                throw new KeyNotFoundException("Cita not found.");
            }

            cita.FechaHora = citaDTO.FechaHora;
            cita.Status = citaDTO.Status;
            cita.UserId = citaDTO.UserId;
            cita.TipoCitaId = citaDTO.TipoCitaId;
            cita.SucursalId = citaDTO.SucursalId;

            _myContext.Citas.Update(cita);
            _myContext.SaveChanges();
        }

        private Cita GetCitaEntityById(int id)
        {
            return _myContext.Citas.FirstOrDefault(c => c.CitaId == id);
        }

        public void CancelarCita(int id)
        {
            var cita = GetCitaEntityById(id);
            if (cita == null)
            {
                throw new KeyNotFoundException("Cita no encontrada.");
            }

            if (cita.FechaHora < DateTime.Now.AddHours(24))
            {
                throw new InvalidOperationException("Las citas se deben cancelar con mínimo 24 horas de antelación.");
            }
            cita.Status = "CANCELADA";
            _myContext.Citas.Update(cita);
            _myContext.SaveChanges();
        }
    }
}
