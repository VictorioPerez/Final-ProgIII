using FinalProg.Data;
using FinalProg.Middleware.Exceptions;
using FinalProg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProg.Services.Imp
{
    public class UserServiceImp : IUserService
    {
        private readonly ParcialDbContext _context;

        public UserServiceImp(ParcialDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ExceptionBadRequestClient("Email y contraseña son requeridos");
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            if (!VerificarContrasena(password, usuario.Password))
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta");
            }

            // Generar token
            string token = GenerarTokenUnico();

            // Crear objeto de token
            var usuarioToken = new TokenXUsuario
            {
                IdUsuario = usuario.Id,
                Token = token,
                DateTimeValid = DateTime.UtcNow.AddMinutes(15)
            };

            _context.TokensXUsuario.Add(usuarioToken);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                mensaje = "Login exitoso",
                token = token
            });
        }

        private string GenerarTokenUnico()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()) +
                   Convert.ToBase64String(Guid.NewGuid().ToByteArray()) +
                   Convert.ToBase64String(Guid.NewGuid().ToByteArray()) +
                   Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                   .Substring(0, 400);
        }

        private bool VerificarContrasena(string contrasenaIngresada, string contrasenaAlmacenada)
        {
            return contrasenaIngresada == contrasenaAlmacenada;
        }
    }
}
