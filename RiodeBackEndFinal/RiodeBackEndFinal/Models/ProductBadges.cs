namespace RiodeBackEndFinal.Models
{
    public class ProductBadges
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product  Product { get; set; }
        public int BadgeId { get; set; }
        public Badge Badge { get; set; }
    }
}
