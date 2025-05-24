using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Domain.DTOs;

namespace TomasosPizzeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            try
            {
                var token = await _userService.Login(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { ex.Message });
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            var result = await _userService.Register(user);

            if (result)
                return Created();
            else
                return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var user = await _userService.GetUserAsync(userId);

            return Ok(user);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAsync(UserDTO user)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            await _userService.UpdateUserAsync(userId, user);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updaterole")]
        public async Task<IActionResult> UpdateUserRoleAsync(string username, string newRole)
        {
            var result = await _userService.UpdateUserRoleAsync(username, newRole);

            if (result) return Ok();
            else return BadRequest();
        }
    }
}
