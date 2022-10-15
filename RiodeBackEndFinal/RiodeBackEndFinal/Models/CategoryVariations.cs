namespace RiodeBackEndFinal.Models
{
    public class CategoryVariations
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } 
        public Category Category { get; set; }  
        public int VariationId  { get; set; }
        public Variation Variation { get; set; }

    }
}
