using DatingApp.API.Data.Entities;

namespace DatingApp_ASP.Data.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        void InsertNewUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        bool IsSaveChanges();
    }
}