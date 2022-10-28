using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using RiodeBackEndFinal.DAL;
using RiodeBackEndFinal.Models;
using RiodeBackEndFinal.ViewModels;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace RiodeBackEndFinal.Controllers
{
    public class ProductController :Controller
    {
        RiodeContext _context { get; }
        public ProductController(RiodeContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Detail(int? id)
        {
            var prod =await _context.Products.Where(p => p.Id == id)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductBadges)
                .ThenInclude(pb => pb.Badge)
                .SingleOrDefaultAsync();
            return View(prod);
        }
        public IActionResult Shop()
        {
            ICollection<Product> products = _context.Products
                .Include(p => p.ProductImages)
                .Include(p=>p.ProductBadges)
                .ThenInclude(pb=>pb.Badge).Take(12).ToList();
            return View(products);
        }
        
        [HttpPost]
        public async Task<IActionResult> FilterProduct([FromForm]FromFilterVM filter)
        {
            IQueryable<Product> products = _context.Products.Include(p=>p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductBadges)
                .ThenInclude(pb => pb.Badge)
                .Include(p => p.ProductColors)
                .ThenInclude(p => p.Color);
            if (filter.CategoryId !=0)
            {
              products=  products.Where(p => p.CategoryId == filter.CategoryId || p.Category.ParentId==filter.CategoryId);
            }
            if (filter.ColorIds != null)
            {
                //foreach (var colorId in filter.ColorIds)
                //{
                //    products = products.Where(p => p.ProductColors.Any(pc=>pc.ColorId==colorId));
                //}
                var productIds = _context.ProductColors.Where(pc => filter.ColorIds.Contains(pc.ColorId)).Select(pc=>pc.ProductId).Distinct().ToList();
                products = products.Where(p => productIds.Contains(p.Id));
            }
            int min = filter.MinPrice;
            int max = filter.MaxPrice;

            if (min<=max && min>0)
            {
                products = products.Where(p => (p.SellPrice * (100 - p.DiscountPercent) / 100) >= min && (p.SellPrice * (100 - p.DiscountPercent) / 100) <= max);
            }
            ICollection<Product> sendprods =await products.Take(12).ToListAsync();
            return PartialView("_ProductsPartialView", sendprods);
            //return View("Shop", sendprods);


        }
    }
}
