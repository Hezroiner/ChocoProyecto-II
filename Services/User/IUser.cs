using Proyecto_II.Entities;

namespace Services
{
    public interface IUser
    {
        public User AddUser(User user);
        public List<User> GetAll();
        public User GetById(int id);

    }
}