using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Proyecto_II.Entities;
using Services;
using Services.MyDbContext;
using System.Net.Mail;
using System.Net;

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
            if (_myContext.Citas.Any(c =>
                c.UserId == cita.UserId &&
                c.FechaHora.Date == cita.FechaHora.Date &&
                c.Status == "ACTIVA"))
            {
                throw new InvalidOperationException("No se puede crear otra cita para el mismo paciente en el mismo día.");
            }
            _myContext.Citas.Add(cita);
            _myContext.SaveChanges();


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

        public List<Cita> GetAll()
        {
            return _myContext.Citas.Include(x=> x.User).Include(x=> x.TipoCita).ToList();
        }

        public Cita GetById(int id)
        {
            var cita = _myContext.Citas
            .Include(cita => cita.User)
            .Include(cita => cita.TipoCita)
            .Include(cita => cita.Sucursal)
            .FirstOrDefault(cita => cita.CitaId == id);

            if (cita == null)
            {
                // Manejo de la situación cuando no se encuentra la entidad cita
                throw new KeyNotFoundException("Cita not found");
            }

            return cita;
        }

        public Cita Update(int id, Cita newCita)
        {
            Cita updateCita = _myContext.Citas.Find(id);

            if (updateCita is not null)
            {
                updateCita.FechaHora = newCita.FechaHora;
                updateCita.Status = newCita.Status;

                _myContext.Citas.Update(updateCita);
                _myContext.SaveChanges();
            }

            return updateCita;
        }

        public void CancelarCita(int id)
        {
            var cita = GetById(id);

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
