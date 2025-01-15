using aktionApp.Entities.Interfaces;
using aktionApp.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace aktionApp.Repos
{
        
    public class CategoriesRepository: ICategoriesRepository
    {
        
            private readonly string _connectionString;

            public CategoriesRepository(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            public async Task<IEnumerable<Categories>> GetAllCategoriesAsync()
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    return await connection.QueryAsync<Categories>("sp_GetAllCategories", commandType: CommandType.StoredProcedure);
                }
            }

    }
}   
