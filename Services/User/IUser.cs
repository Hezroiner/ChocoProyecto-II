using Entitites;
using Proyecto_II.Entities;

namespace Services
{
    public interface IUser
    {
        public void Register(UserRegisterModel model);
        public string Login(UserLoginModel model);
        public User AddUser(User user);
        public List<User> GetAll();
        public User GetById(int id);

    }
}