using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.IRepository
{
    public interface IAppDbContext
    {
        SqlConnection CreateConnection();
    }
}
