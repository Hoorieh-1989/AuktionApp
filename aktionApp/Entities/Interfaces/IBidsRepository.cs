namespace aktionApp.Entities.Interfaces
{
    public interface IBidsRepository
    {
        Task<IEnumerable<Bids>> GetBidsForAuctionAsync(int auctionId);
        Task<Bids?> GetHighestBidAsync(int auctionId);
        Task AddBidAsync(Bids bid);
        Task RemoveBidAsync(int bidId);
    }
}
