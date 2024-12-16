using FinalProg.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController
    {
        private IUserService _userService;

        public UsuarioController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            return await _userService.Login(email, password);
        }
    }
}
