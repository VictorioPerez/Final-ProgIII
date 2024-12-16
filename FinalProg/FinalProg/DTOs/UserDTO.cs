using FinalProg.Models;

namespace FinalProg.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string NombreRol { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaAlta { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public UserDTO(Usuario usuario)
        {
            Id = usuario.Id;
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            Email = usuario.Email;
            Password = usuario.Password;
            FechaNacimiento = usuario.FechaNacimiento;
            NombreRol = usuario.Rol?.Nombre;
            Activo = usuario.Activo;
            FechaAlta = usuario.FechaAlta;
            FechaModificacion = usuario.FechaModificacion;
        }
    }
}
