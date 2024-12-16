using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProg.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
