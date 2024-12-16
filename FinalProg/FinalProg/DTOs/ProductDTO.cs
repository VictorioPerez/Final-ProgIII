using FinalProg.Models;

namespace FinalProg.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public Guid IdCategoria { get; set; }

        public string NombreCategoria { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public ProductDTO()
        {
             
        }

        public ProductDTO(Producto product)
        {
            Id = product.Id;
            Nombre = product.Nombre;
            Descripcion = product.Descripcion;
            IdCategoria = product.IdCategoria;
            NombreCategoria = product.Categoria?.Nombre;
            FechaCreacion = product.FechaCreacion;
            FechaModificacion = product.FechaModificacion;
        }
    }
}
