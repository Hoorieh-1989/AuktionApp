namespace aktionApp.Entities
{
    public class Bids
    {
        public int BidId { get; set; }
        public decimal Amount { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }
    }
}
