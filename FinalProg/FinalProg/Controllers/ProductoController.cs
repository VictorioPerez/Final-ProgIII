using FinalProg.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("getWithTake")]
        public async Task<IActionResult> getWithTake([FromHeader(Name = "Authorization")] string token, [FromQuery] int take)
        {
            if (token != null && token.StartsWith("Bearer "))
            {
                token = token.Substring(7);
            }

            return Ok(await _productoService.GetWithTake(token, take));
        }

        [HttpGet("getById/{productId}")]
        public async Task<IActionResult> getById([FromHeader(Name = "Authorization")] string token, [FromRoute] string productId)
        {
            if (token != null && token.StartsWith("Bearer "))
            {
                token = token.Substring(7);
            }

            return Ok(await _productoService.GetById(productId, token));
        }

    }
}
