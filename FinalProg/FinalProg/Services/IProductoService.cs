
using FinalProg.DTOs;

namespace FinalProg.Services
{
    public interface IProductoService
    {
        Task<ProductDTO> GetById(string productId, string? token);
        Task<List<ProductDTO>> GetWithTake(string? token, int take);
    }
}
