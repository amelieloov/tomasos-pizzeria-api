using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Domain.DTOs;

namespace TomasosPizzeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(List<DishAddDTO> dishDtos)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                await _service.AddOrderAsync(userId, dishDtos);

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrdersByUserAsync()
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                var orders = await _service.GetOrdersByUserAsync(userId);

                return Ok(orders);
            }
            catch (Exception)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("updatestatus")]
        public async Task<IActionResult> UpdateStatus(int orderId, string newStatus)
        {
            try
            {
                await _service.UpdateStatusAsync(orderId, newStatus);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderAsync(int orderId)
        {
            try
            {
                await _service.DeleteOrderAsync(orderId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
