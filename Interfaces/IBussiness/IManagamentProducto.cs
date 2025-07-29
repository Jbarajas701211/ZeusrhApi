using Models;
using Models.DTOs;
using Models.Entities;

namespace Interfaces.IBussiness
{
    public interface IManagamentProducto
    {
        Task<ApiResponse<ProductoDTO>> ObtenerProductoPorId(int productId);
        Task<ApiResponse<ProductoDTO>> CrearProducto(Producto producto);
    }
}
