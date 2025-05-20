using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Data.Identity;
using TomasosPizzeria.Domain.DTOs;

namespace TomasosPizzeria.Core.Services
{
    public class UserService : IUserService
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;

        public UserService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string?> Login(LoginDTO user)
        {
            var result = await _signInManager
                 .PasswordSignInAsync(user.UserName, user.Password,
                                        false, false);

            var appUser = await _userManager.FindByNameAsync(user.UserName);
            if (appUser == null)
            {
                //Error handling
            }

            var roles = await _userManager.GetRolesAsync(appUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = GenerateJwtToken(claims);

            return token;
        }

        public async Task<bool> Register(UserDTO user)
        {
            var newUser = new ApplicationUser()
            {
                UserName = user.UserName
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            return result.Succeeded;
        }

        public string GenerateJwtToken(List<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        public async Task UpdateUserAsync(string userId, UserDTO userDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userDto.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Password update failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            await _userManager.UpdateAsync(user);
        }


        public async Task<UserDTO> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var userDto = new UserDTO()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return userDto;
        }
    }
}
