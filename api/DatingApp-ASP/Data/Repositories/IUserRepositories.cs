using DatingApp.API.Data.Entities;

namespace DatingApp_ASP.Data.Repositories
{
    public interface IUserRepositories
    {
        List<User> GetUsers();
        User GetUser(int id);
        User GetUserByUsername(string username);
        User InsertNewUser(User user);
        User UpdateUser(User user);
        void DeleteUser(User user);
    }
}