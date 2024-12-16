using FinalProg.DTOs;
using FinalProg.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IUserService _userService;

        public UsuarioController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            return Ok(await _userService.Login(email, password));
        }

        [HttpGet("getById/{userId}")]
        public async Task<IActionResult> getById([FromHeader(Name = "Authorization")] string token, [FromRoute] string userId)
        {
            if (token != null && token.StartsWith("Bearer "))
            {
                token = token.Substring(7);
            }

            return Ok(await _userService.GetById(userId, token));
        }

        [HttpPost("register")]
        public async Task<IActionResult> create([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _userService.Register(request));
        }
    }
}
