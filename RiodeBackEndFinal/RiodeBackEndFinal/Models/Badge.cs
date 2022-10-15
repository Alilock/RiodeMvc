using RiodeBackEndFinal.Models.Base;

namespace RiodeBackEndFinal.Models
{
    public class Badge:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductBadges> ProductBadges { get; set; }
    }
}
