using Proyecto_II.Entities;

namespace Proyecto_II.Services
{
    public interface IUser
    {
        public User Add(User user);

        public List<User> GetAll();
        public User GetById(int id);

        public void Update(int id, User user);
        public void Delete(int id);
    }
}
