using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Domain.DTOs;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;

        public DishController(IDishService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDishesAsync()
        {
            var dishes = await _service.GetDishesAsync();

            return Ok(dishes);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDishAsync(DishDTO dish)
        {
            try
            {
                await _service.AddDishAsync(dish);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateDishAsync(int dishId, DishDTO dishDto)
        {
            try
            {
                await _service.UpdateDishAsync(dishId, dishDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
