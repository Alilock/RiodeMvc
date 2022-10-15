using RiodeBackEndFinal.Models.Base;

namespace RiodeBackEndFinal.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public double CostPrice { get; set; }
        public int DiscountPercent { get; set; }
        // Category Relation o-m
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        
        // Badge Relation m-m
        public ICollection<ProductBadges> ProductBadges { get; set; }
        // Variation_Option Relation m-m
        public ICollection<ProductVariations> ProductVariations { get; set; }

    }
}
