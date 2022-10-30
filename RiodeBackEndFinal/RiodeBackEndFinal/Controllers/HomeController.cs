using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiodeBackEndFinal.DAL;
using RiodeBackEndFinal.Models;
using RiodeBackEndFinal.ViewModels;
using Z.EntityFramework.Plus;

namespace RiodeBackEndFinal.Controllers
{
    public class HomeController : Controller
    {
        RiodeContext _context { get; }
        public HomeController(RiodeContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Sliders = _context.Sliders.Where(s=>s.IsDisable==false);
            ICollection<Product> products = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductBadges)
                .ThenInclude(p => p.Badge).ToList();
            homeVM.Categories = _context.Categories.Where(c=>c.IsDisable==false).OrderByDescending(c => c.Products.Count()).Take(4).ToList();
            homeVM.Products = _context.Products
                .Include(p=>p.Category)
                .Include(p=>p.ProductImages)
                .Include(p =>p.Reviews)
                .Include(p=>p.ProductBadges)
                .ThenInclude(p=>p.Badge).ToList();
            return View(homeVM);    
        }

    }
}
