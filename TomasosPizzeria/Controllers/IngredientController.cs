using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Core.Interfaces;

namespace TomasosPizzeria.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _service;

        public IngredientController(IIngredientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredients()
        {
            var ingredients = await _service.GetIngredientsAsync();

            return Ok(ingredients);
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(string name)
        {
            await _service.AddIngredientAsync(name);

            return Created();
        }
    }
}
