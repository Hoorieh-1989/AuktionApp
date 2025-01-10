namespace aktionApp.Entities.Interfaces
{
    public interface IAuctionsRepository
    {
        Task<IEnumerable<Auctions>> GetAllAuctionsAsync();
        Task<Auctions> GetAuctionByIdAsync(int auctionId);
        Task AddAuctionAsync(Auctions auction);
        Task UpdateAuctionAsync(Auctions auction);
        Task DeleteAuctionAsync(int auctionId);
    }
}
