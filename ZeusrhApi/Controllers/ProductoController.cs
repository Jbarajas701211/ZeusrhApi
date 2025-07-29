using AutoMapper;
using Interfaces.IBussiness;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
using Models.Entities;

namespace ZeusrhApi.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private readonly IManagamentProducto managamentProducto;
        private readonly IMapper _mapper;

        public ProductoController(IManagamentProducto managamentProducto, IMapper mapper)
        {
            this.managamentProducto = managamentProducto;
            _mapper = mapper;
        }

        [HttpGet(Name = "ObtenerProductos")]
        public async Task<ApiResponse<ProductoDTO>> Get(int idProducto)
        {
            return await managamentProducto.ObtenerProductoPorId(idProducto);
        }

        [HttpPost(Name = "CrearProducto")]
        public async Task<ApiResponse<ProductoDTO>> Post(ProductoDTO productoDTO)
        {
            var producto = _mapper.Map<Producto>(productoDTO);

            try
            {
                return await managamentProducto.CrearProducto(producto);

            }
            catch (Exception ex)
            {

                return new ApiResponse<ProductoDTO> { Success = false, Errors = new List<string> { ex.Message } };
            }
        }
    }
}
