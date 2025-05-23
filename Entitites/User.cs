﻿namespace Proyecto_II.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
        public List<Cita> Citas { get; set; }
    }
}
