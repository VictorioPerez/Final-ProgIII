using Microsoft.AspNetCore.Mvc;

namespace FinalProg.Services
{
    public interface IUserService
    {
        Task<IActionResult> Login(string email, string password);
    }
}
