using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Data.Identity;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly PizzaAppContext _context;
        private UserManager<ApplicationUser> _userManager;

        public UserRepo(PizzaAppContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            return await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            var userOrg = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == user.Id);

            _context.Users.Entry(userOrg).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }
    }
}
