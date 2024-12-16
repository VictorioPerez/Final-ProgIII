using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProg.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [ForeignKey(nameof(Rol))]
        public Guid IdRol { get; set; }

        public Rol Rol { get; set; }

        [Required]
        public bool Activo { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public ICollection<TokenXUsuario> Tokens { get; set; }
    }

}
