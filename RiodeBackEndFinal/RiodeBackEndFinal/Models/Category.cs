using RiodeBackEndFinal.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiodeBackEndFinal.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string ImageName { get; set; }
        public Category Parent { get; set; }
        public ICollection<CategoryVariations> CategoryVariations{ get; set; }
        public ICollection<Category> Children { get; set; }
        public ICollection<Product> Products { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
