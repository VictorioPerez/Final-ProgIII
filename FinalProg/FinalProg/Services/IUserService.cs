using FinalProg.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinalProg.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetById(string userId);
        Task<string> Login(string email, string password);
        Task<UserDTO> Register(UserRequest request);
    }
}
