using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Domain.DTOs;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Api.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpPost]
        public async Task<IActionResult> AddDishAsync(DishDTO dish)
        {
            await _service.AddDishAsync(dish);

            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDishAsync(int dishId, DishDTO dishDto)
        {
            await _service.UpdateDishAsync(dishId, dishDto);

            return Ok();
        }
    }
}
