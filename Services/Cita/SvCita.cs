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

        public CitaDTO AddCita(CitaPostDTO citaPostDTO)
        {
            if (_myContext.Citas.Any(c =>
                c.UserId == citaPostDTO.UserId &&
                c.FechaHora.Date == citaPostDTO.FechaHora.Date &&
                c.Status == "ACTIVA"))
            {
                throw new InvalidOperationException("No se puede crear otra cita para el mismo paciente en el mismo día.");
            }

            // Crear la nueva cita
            var cita = new Cita
            {
                FechaHora = citaPostDTO.FechaHora,
                Status = "ACTIVA",
                UserId = citaPostDTO.UserId,
                TipoCitaId = citaPostDTO.TipoCitaId,
                SucursalId = citaPostDTO.SucursalId
            };

            // Agregar la nueva cita a la base de datos
            _myContext.Citas.Add(cita);
            _myContext.SaveChanges();

            // Llamar a la función para enviar el correo electrónico
            SendEmail(cita);

            // Mapear la entidad Cita al DTO
            var citaDTO = new CitaDTO
            {
                CitaId = cita.CitaId,
                FechaHora = cita.FechaHora,
                Status = cita.Status,
                UserId = cita.UserId,
                TipoCitaId = cita.TipoCitaId,
                SucursalId = cita.SucursalId
            };

            return citaDTO;
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
            var cita = _myContext.Citas
                         .Include(c => c.User)
                         .Include(c => c.TipoCita)
                         .Include(c => c.Sucursal)
                         .FirstOrDefault(c => c.CitaId == id);

            if (cita == null) return null;

            return new CitaDTO
            {
                CitaId = cita.CitaId,
                FechaHora = cita.FechaHora,
                Status = cita.Status,
                UserId = cita.User.UserId, 
                UserName = cita.User.Nombre,
                TipoCitaId = cita.TipoCita.TipoCitaId,
                TipoCitaNombre = cita.TipoCita.Nombre,
                SucursalId = cita.Sucursal.SucursalId,
                SucursalNombre = cita.Sucursal.Nombre
            };
        }

        public List<CitaDTO> GetCitaByUserId(int userId)
        {
            var citas = _myContext.Citas
                                  .Include(c => c.User)
                                  .Include(c => c.TipoCita)
                                  .Include(c => c.Sucursal)
                                  .Where(c => c.UserId == userId)
                                  .ToList();

            if (citas == null || citas.Count == 0) return new List<CitaDTO>();

            return citas.Select(cita => new CitaDTO
            {
                CitaId = cita.CitaId,
                FechaHora = cita.FechaHora,
                Status = cita.Status,
                UserId = cita.User.UserId,  // Use null conditional operator to avoid null reference exceptions
                UserName = cita.User.Nombre,
                TipoCitaId = cita.TipoCita.TipoCitaId,
                TipoCitaNombre = cita.TipoCita.Nombre,
                SucursalId = cita.Sucursal.SucursalId,
                SucursalNombre = cita.Sucursal.Nombre
            }).ToList();
        }




        public CitaDTO UpdateCita(int id, CitaPostDTO citaPostDto)
        {
            var cita = _myContext.Citas.FirstOrDefault(c => c.CitaId == id);
            if (cita == null)
            {
                throw new KeyNotFoundException("Cita no encontrada.");
            }

            if (_myContext.Citas.Any(c =>
                c.UserId == citaPostDto.UserId &&
                c.FechaHora.Date == citaPostDto.FechaHora.Date &&
                c.Status == "ACTIVA" &&
                c.CitaId != id))
            {
                throw new InvalidOperationException("No se puede crear otra cita para el mismo paciente en el mismo día.");
            }

            // Actualizar los campos de la cita existente
            cita.FechaHora = citaPostDto.FechaHora;
            cita.UserId = citaPostDto.UserId;
            cita.TipoCitaId = citaPostDto.TipoCitaId;
            cita.SucursalId = citaPostDto.SucursalId;

            _myContext.Citas.Update(cita);
            _myContext.SaveChanges();

            // Mapear la entidad Cita al DTO
            var citaDTO = new CitaDTO
            {
                CitaId = cita.CitaId,
                FechaHora = cita.FechaHora,
                Status = cita.Status,
                UserId = cita.UserId,
                TipoCitaId = cita.TipoCitaId,
                SucursalId = cita.SucursalId
            };

            return citaDTO;
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

            if (cita.FechaHora > DateTime.Now.AddHours(24))
            {
                throw new InvalidOperationException("Las citas se deben cancelar con mínimo 24 horas de antelación.");
            }
            cita.Status = "CANCELADA";
            _myContext.Citas.Update(cita);
            _myContext.SaveChanges();
        }
    }
}
