using RiodeBackEndFinal.Models;

namespace RiodeBackEndFinal.ViewModels
{
    public class FilterItemsVM
    {
        public ICollection<Category> Categories { get; set; }
        public ICollection<Color> Colors { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
