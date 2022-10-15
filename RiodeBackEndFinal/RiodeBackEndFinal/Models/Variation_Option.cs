namespace RiodeBackEndFinal.Models
{
    public class Variation_Option
    {
        public int Id { get; set; }
        public int VariationId { get; set; }
        public string Value { get; set; }
        public Variation Variation { get; set; }
        public ICollection<ProductVariations> ProductVariations { get; set; }
        
    }
}
