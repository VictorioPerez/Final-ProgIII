﻿using FinalProg.Data;
using FinalProg.DTOs;
using FinalProg.Middleware.Exceptions;
using FinalProg.Models;
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

        public async Task<UserDTO> GetById(string userId, string token)
        {
            await ValidarToken(token);

            if (!Guid.TryParse(userId, out Guid guidId))
            {
                throw new ExceptionBadRequestClient("El formato de userId no es válido. Debe ser un GUID.");
            }

            var usuario = await _context.Usuarios.Include(x => x.Rol).FirstOrDefaultAsync(x => x.Id == guidId);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }
            return new UserDTO(usuario);
        }

        public async Task<string> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ExceptionBadRequestClient("Email y contraseña son requeridos");
            }

            var usuario = await _context.Usuarios.Include(x => x.Tokens)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null || !usuario.Activo)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            if (!VerificarContrasena(password, usuario.Password))
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta");
            }

            var usuarioToken = new TokenXUsuario();
            if (usuario.Tokens == null || usuario.Tokens.Count <= 0)
            {

                string token = GenerarTokenUnico();

                usuarioToken = new TokenXUsuario
                {
                    IdUsuario = usuario.Id,
                    Token = token,
                    DateTimeValid = DateTime.UtcNow.AddMinutes(15)
                };
            }
            else
            {
                usuario.Tokens.Clear();

                string token = GenerarTokenUnico();

                usuarioToken = new TokenXUsuario
                {
                    IdUsuario = usuario.Id,
                    Token = token,
                    DateTimeValid = DateTime.UtcNow.AddMinutes(15)
                };
            }

            _context.TokensXUsuario.Add(usuarioToken);
            await _context.SaveChangesAsync();

            return usuarioToken.Token;
        }

        public async Task<UserDTO> Register(UserRequest request)
        {
            var checkUser = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (checkUser == null)
            {
                throw new ExceptionBadRequestClient("El usuario con el mail ya se encuentra registrado");
            }

            var checkRol = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.IdRol);

            if (checkRol == null)
            {
                throw new ExceptionBadRequestClient("El rol ingresado no existe");
            }

            var nuevoUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                Password = request.Password,
                FechaNacimiento = request.FechaNacimiento,
                IdRol = request.IdRol,
                Activo = true,
                FechaAlta = DateTime.UtcNow
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return new UserDTO(nuevoUsuario);
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

        public async Task ValidarToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new UnauthorizedAccessException("Token no proporcionado");
            }

            var tokenRegistrado = await _context.TokensXUsuario
                .FirstOrDefaultAsync(t => t.Token == token);

            if (tokenRegistrado == null)
            {
                throw new UnauthorizedAccessException("Token inválido");
            }

            if (tokenRegistrado.DateTimeValid < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Token expirado");
            }
        }
    }
}
