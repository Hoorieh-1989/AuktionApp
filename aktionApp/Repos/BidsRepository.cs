using aktionApp.Entities;
using aktionApp.Entities.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace aktionApp.Repos
{
    public class BidsRepository : IBidsRepository
    {
            private readonly string _connectionString;

            public BidsRepository(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            public async Task<IEnumerable<Bids>> GetBidsForAuctionAsync()
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    return await connection.QueryAsync<Bids>("sp_GetAllBids", commandType: CommandType.StoredProcedure);
                }
            }

            
        

    }
}
