using Interfaces.IRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContext
{
    public class AppDbContext : IAppDbContext
    {
        private readonly string _connectionString;

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
