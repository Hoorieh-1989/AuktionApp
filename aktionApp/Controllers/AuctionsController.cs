using aktionApp.DTOs;
using aktionApp.Entities;
using aktionApp.Entities.Interfaces;
using aktionApp.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuktionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionsRepository _auctionRepository;

        //Konstruktor för att injicera beroende
        public AuctionsController(IAuctionsRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        //Hämta en auktion med ID
        [HttpGet]
        public async Task<IActionResult> GetAllAuctions()
        {
            var auctions = await _auctionRepository.GetAllAuctionsAsync();
            return Ok(auctions); //Returnerar lista med auktioner
        }

        //Skapa en ny auktion
        [HttpPost]
        [Authorize] //Kräver inloggning
        public async Task<IActionResult> CreateAuction(AuctionsDto auctionDto)
        {
            //Skapa ett nytt auktionsobjekt
            var auction = new Auctions
            {
                Title = auctionDto.Title,
                Description = auctionDto.Description,
                Price = auctionDto.Price,
                StartDateTime = auctionDto.StartDateTime,
                EndDateTime = auctionDto.EndDateTime,
                CreatedByUserId = auctionDto.CreatedByUserId
            };

            //Lägg till auktionen i databasen
            await _auctionRepository.AddAuctionAsync(auction);
            return CreatedAtAction(nameof(GetAllAuctions), auction);
        }
    }
}
