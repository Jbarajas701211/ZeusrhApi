using AutoMapper;
using Interfaces.IBussiness;
using Interfaces.IRepository;
using Models;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Bussiness
{
    public class ManagementProducto : IManagamentProducto
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public ManagementProducto(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductoDTO>> ObtenerProductoPorId(int productId)
        {
            try
            {
                var producto = await _productoRepository.ObtenerProductoPorId(productId);

                if(producto is null)
                {
                    return new ApiResponse<ProductoDTO>() { Errors = new List<string> { "Producto no localizado" } };
                }

                var productoDto = _mapper.Map<ProductoDTO>(producto);
                return new ApiResponse<ProductoDTO> { Success = true, Data = productoDto };
            }
            catch (Exception ex)
            {

                return new ApiResponse<ProductoDTO> { Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<ApiResponse<ProductoDTO>> CrearProducto(Producto producto)
        {
            try
            {
                var seCreoProducto = await _productoRepository.CrearProductoAsync(producto);
                if (seCreoProducto)
                {
                    var productoDto = _mapper.Map<ProductoDTO>(producto);
                    return new ApiResponse<ProductoDTO> { Success = true, Data = productoDto };
                }
                return new ApiResponse<ProductoDTO> { Success = false, Errors = new List<string>() { "Producto no generado, verifique" } };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<ProductoDTO> { Errors = new List<string> { ex.Message } };
            }
        }
    }
}
