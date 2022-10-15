using RiodeBackEndFinal.DAL;
using RiodeBackEndFinal.Models;

namespace RiodeBackEndFinal.Areas.Admin.ViewModels
{
    public class CategoryVM
    {
        public ICollection<Category> Categories { get; set; }
        public  Category Category { get; set; }
        public RiodeContext Context { get; }

     
        
       
    }
}
