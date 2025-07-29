using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
using Models.Entities;

namespace ZeusrhApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {

        [HttpGet(Name = "ObtenerProductos")]
        public async Task<ApiResponse<ProductoDTO>> Get()
        {

        }
    }
}
