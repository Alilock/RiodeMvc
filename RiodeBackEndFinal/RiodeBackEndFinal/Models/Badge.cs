using RiodeBackEndFinal.Models.Base;

namespace RiodeBackEndFinal.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductBadges> ProductBadges { get; set; }
    }
}
