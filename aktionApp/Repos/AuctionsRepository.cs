using aktionApp.Entities.Interfaces;
using aktionApp.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace aktionApp.Repos
{
    public class AuctionsRepository : IAuctionsRepository
    {
        private readonly string _connectionString;

        public AuctionsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Auctions>> GetAllAuctionsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Auctions>("sp_GetAllAuctions", commandType: CommandType.StoredProcedure);
            }
        }

        //Implementera fler metoder: GetAuctionByIdAsync, AddAuctionAsync, etc.
    }
}
