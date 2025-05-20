using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Core.Interfaces;

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

        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(List<int> dishIds)
        {

            _service.AddOrderAsync();

            return Created();
        }
    }
}
