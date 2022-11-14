
using DatingApp.API.Data;
using DatingApp.API.Data.Entities;

namespace DatingApp_ASP.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User GetUserById(int id)
        {
            return _context.AppUsers.FirstOrDefault(user => user.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.AppUsers.FirstOrDefault(user => user.Username == username);
        }

        public List<User> GetUsers()
        {
            return _context.AppUsers.ToList();
        }

        public void InsertNewUser(User user)
        {
            _context.AppUsers.Add(user);
        }

        public void UpdateUser(User user)
        {
            _context.AppUsers.Update(user);
        }
        public void DeleteUser(User user)
        {
            _context.AppUsers.Remove(user);
        }
        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}