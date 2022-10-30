namespace RiodeBackEndFinal.Models
{
    public class Review
    {
        public int Id { get; set; } 
        public string Content { get; set; }
        public int Raiting { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ProductId  { get; set; }
        public Product Product { get; set; }

    }
}
