namespace RiodeBackEndFinal.Models
{
    public class ProductVariations
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Variation_OptionId { get; set; }
        public Variation_Option Variation_Option { get; set; }
        public int StockCount { get; set; }
        public double SellPrice { get; set; }

    }
}
