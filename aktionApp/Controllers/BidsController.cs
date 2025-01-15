using aktionApp.Entities.Interfaces;
using aktionApp.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuktionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IBidsRepository _BidsRepository;

        public BidsController(IBidsRepository bidRepository) => _BidsRepository = bidRepository;

        //Ta fram alla bud från en specifik auktion
        [HttpGet("auction/{auctionId}")]
        public async Task<IActionResult> GetBidsForAuction(int auctionId)
        {
            var bids = await _bidRepository.GetBidsForAuctionAsync(auctionId);
            return Ok(bids);
        }

        //Placera ett nytt bid
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceBid(BidCreateDto bidDto)
        {
        private readonly IBidsRepository _bidRepository;

        //Konstruktor för att injicera beroende
        public BidsController(IBidsRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        //Placera ett nytt bud
        [HttpPost]
        [Authorize] //Kräver inloggning
        public async Task<IActionResult> PlaceBid(BidCreateDto bidDto)
        {
            //Hämta högsta budet för auktionen
            var highestBid = await _bidRepository.GetHighestBidAsync(bidDto.AuctionId);

            //Kontrollera att det nya budet är högre än det befintliga högsta budet
            if (highestBid != null && bidDto.Price <= highestBid.Price)
            {
                return BadRequest("Ditt bud måste vara högre än det aktuella högsta budet.");
            }

            //Skapa ett nytt bud och spara det
            var bid = new Bid
            {
                AuctionId = bidDto.AuctionId,
                UserId = bidDto.UserId,
                Price = bidDto.Price,
                Timestamp = bidDto.Timestamp
            };

            await _bidRepository.AddBidAsync(bid);
            return Ok("Bud lagt framgångsrikt.");
        }
    }
}
