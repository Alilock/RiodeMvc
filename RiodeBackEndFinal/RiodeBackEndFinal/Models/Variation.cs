using RiodeBackEndFinal.Models.Base;

namespace RiodeBackEndFinal.Models
{
    public class Variation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Variation_Option> Variation_Options { get; set; }
    }
}
