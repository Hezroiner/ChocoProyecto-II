namespace Proyecto_II.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Nombre { get; set; }
        public List<User>? Users { get; set; }
    }
}
