using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TomasosPizzeria.Data.Identity;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Domain.DTOs;

namespace TomasosPizzeria.Core.Services
{
    public class UserService
    {
        private readonly IUserRepo _repo;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;

        public UserService(IUserRepo repo, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _repo = repo;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> Login(UserDTO user)
        {
            var result = await _signInManager
                 .PasswordSignInAsync(user.UserName, user.Password,
                                        false, false);

            var appUser = new ApplicationUser()
            {
                UserName = user.UserName,
                PasswordHash = user.Password
            };

            //Här vill vi oftast skapa en JWT token som även innehåller vilken 
            //behörighet användaren har. Behörighet kan hämtas med tex

            var token = GenerateJwtToken(appUser);

            return result.Succeeded;
        }

        public async Task<bool> Register(UserDTO user)
        {
            var newUser = new ApplicationUser()
            {
                UserName = user.UserName
                //PasswordHash = user.Password
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            return result.Succeeded;
        }

        //public string GenerateJwtToken(ApplicationUser user)
        //{
        //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var issuer = _configuration["Jwt:Issuer"];
        //    var audience = _configuration["Jwt:Audience"];

        //    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //    var claims = _userManager.GetClaimsAsync(user);
        //    var roles = _userManager.GetRolesAsync(user);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Role, user.RoleName.ToString())
        //    };

        //    var tokenOptions = new JwtSecurityToken(
        //        issuer: issuer,
        //        audience: audience,
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(60),
        //        signingCredentials: signingCredentials
        //        );

        //    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        //    return token;
        //}
    }
}
