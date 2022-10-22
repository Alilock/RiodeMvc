using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiodeBackEndFinal.DAL;

namespace RiodeBackEndFinal.Controllers
{
    public class ProductController :Controller
    {
        RiodeContext _context { get; }
        public ProductController(RiodeContext context)
        {
            _context = context;
        }


        public IActionResult Detail(int? id)
        {
            var prod = _context.Products.Where(p => p.Id == id)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductBadges)
                .ThenInclude(pb => pb.Badge)
                .SingleOrDefault();
            return View(prod);
        }
    }
}
