using TomasosPizzeria.Data.Identity;

namespace TomasosPizzeria.Core.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(string username);
        Task AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(int userId);
    }
}
