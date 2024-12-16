using FinalProg.Data;
using FinalProg.DTOs;
using FinalProg.Middleware.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FinalProg.Services.Imp
{
    public class ProductoServiceImp : IProductoService
    {
        private readonly ParcialDbContext _context;
        private readonly IUserService _userService;

        public ProductoServiceImp(ParcialDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ProductDTO> GetById(string productId, string? token)
        {
            await _userService.ValidarToken(token);

            if (!Guid.TryParse(productId, out Guid guidId))
            {
                throw new ExceptionBadRequestClient("El formato de productId no es válido. Debe ser un GUID.");
            }

            var product = await _context.Productos.Include(x => x.Categoria).FirstOrDefaultAsync(x => x.Id == guidId);

            if (product == null)
            {
                throw new KeyNotFoundException("Producto no encontrado");
            }
            return new ProductDTO(product);
        }

        public async Task<List<ProductDTO>> GetWithTake(string? token, int take = 10)
        {
            await _userService.ValidarToken(token);

            var productos = await _context.Productos
                .Include(x => x.Categoria)
                .OrderBy(p => p.FechaCreacion)
                .Take(take)                    
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    IdCategoria = p.IdCategoria,
                    NombreCategoria = p.Categoria.Nombre,
                    FechaCreacion = p.FechaCreacion,
                    FechaModificacion = p.FechaModificacion
                })
                .ToListAsync();

            return productos;
        }
    }
}
