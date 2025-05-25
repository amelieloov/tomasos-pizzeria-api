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
        private readonly IUserService _service;

        public UserController(IUserService _service)
        {
            this._service = _service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            try
            {
                var token = await _service.Login(user);
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
            var result = await _service.Register(user);

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
            var user = await _service.GetUserAsync(userId);

            return Ok(user);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAsync(UserDTO user)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            await _service.UpdateUserAsync(userId, user);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updaterole")]
        public async Task<IActionResult> UpdateUserRoleAsync(string username, string newRole)
        {
            var result = await _service.UpdateUserRoleAsync(username, newRole);

            if (result) return Ok();
            else return BadRequest();
        }
    }
}
