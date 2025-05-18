using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly PizzaAppContext _context;

        public UserRepo(PizzaAppContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == userId);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetUserAsync(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            var userOrg = _context.Users.SingleOrDefault(u => u.UserId == user.UserId);

            _context.Users.Entry(userOrg).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }
    }
}
