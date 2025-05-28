using TomasosPizzeria.Data.Identity;
using TomasosPizzeria.Domain.DTOs;

namespace TomasosPizzeria.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(UserDTO user);
        Task<string?> Login(LoginDTO user);
        Task UpdateUserAsync(string userId, UserUpdateDTO userDto);
        Task<UserReadDTO> GetUserAsync(string userId);
        Task<bool> UpdateUserRoleAsync(string username, string newRole);

    }
}
