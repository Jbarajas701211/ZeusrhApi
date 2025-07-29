using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ProductoDTO
    {
        public string? Nombre { get; set; }
        public string? Marcas { get; set; }
        public decimal Precio { get; set; } = 0;
    }
}
