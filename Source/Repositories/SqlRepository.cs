using Dapper;
using Home.Models.DTOs;
using Home.Models.Entities.Money;
using Microsoft.Data.SqlClient;

namespace Home.Source.Repositories
{
    public partial class SqlRepository
    {
        private readonly string connectionString;

        public SqlRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
    }
}
