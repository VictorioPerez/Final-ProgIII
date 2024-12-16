using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProg.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [ForeignKey(nameof(Categoria))]
        public Guid IdCategoria { get; set; }

        public Categoria Categoria { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
