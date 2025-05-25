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
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            await _service.AddOrderAsync(userId, dishDtos);

            return Created();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrdersByUserAsync()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var orders = await _service.GetOrdersByUserAsync(userId);

            return Ok(orders);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateStatus(int orderId, string newStatus)
        {
            await _service.UpdateStatusAsync(orderId, newStatus);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderAsync(int orderId)
        {
            await _service.DeleteOrderAsync(orderId);

            return Ok();
        }
    }
}
