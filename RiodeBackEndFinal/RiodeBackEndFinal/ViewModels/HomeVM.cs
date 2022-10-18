using RiodeBackEndFinal.Models;

namespace RiodeBackEndFinal.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
