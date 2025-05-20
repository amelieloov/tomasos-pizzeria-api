
using TomasosPizzeria.Data.Identity;

namespace TomasosPizzeria.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<ApplicationUser> GetUserAsync(string userId);
        Task UpdateUserAsync(ApplicationUser user);
    }
}
