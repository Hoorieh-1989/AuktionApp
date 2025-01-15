using aktionApp.Entities;
using aktionApp.Entities.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace aktionApp.Repos
{
    public class UsersRepository: IUsersRepository
    {
        private readonly string _connectionString;

        public UsersRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Users>("sp_GetAllUsers", commandType: CommandType.StoredProcedure);
            }
        }
    }
}
