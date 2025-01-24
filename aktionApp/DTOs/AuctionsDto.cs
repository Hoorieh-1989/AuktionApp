namespace aktionApp.DTOs
{
    public class AuctionsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int CategoryId { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
