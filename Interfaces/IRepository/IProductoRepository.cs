using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.IRepository
{
    public interface IProductoRepository
    {
        Task<Producto> ObtenerProductoPorId(int idProducto);
        Task<bool> CrearProductoAsync(Producto producto);
    }
}
