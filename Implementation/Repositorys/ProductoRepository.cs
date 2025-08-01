﻿using Interfaces.IRepository;
using Microsoft.Data.SqlClient;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Repositorys
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IAppDbContext _context;
        public ProductoRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Producto> ObtenerProductoPorId(int idProducto)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM Producto WHERE IdProducto = @IdProducto";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Producto
                        {
                            IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                            Nombre = reader["Nombre"] as string,
                            Marcas = reader["Marcas"] as string,
                            Precio = reader.GetDecimal(reader.GetOrdinal("Precio"))
                        };
                    }
                }
            }
            return null;
        }

        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = @"INSERT INTO Producto 
                                (Nombre, Marcas, Precio)
                                Values(
                                @Nombre, 
                                @Marcas,
                                @Precio)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Marcas", producto.Marcas);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                await conn.OpenAsync();
                var creado = await cmd.ExecuteNonQueryAsync();
                return creado > 0;
            }
        }
    }
}
