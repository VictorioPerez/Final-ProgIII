using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProg.Models
{
    [Table("TokenXUsuario")]
    public class TokenXUsuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime DateTimeValid { get; set; }

        [ForeignKey(nameof(Usuario))]
        public Guid IdUsuario { get; set; }

        public Usuario Usuario { get; set; }
    }
}
