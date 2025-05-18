using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<ApplicationUser> GetUserAsync(string username);
        Task AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(int userId);
    }
}
