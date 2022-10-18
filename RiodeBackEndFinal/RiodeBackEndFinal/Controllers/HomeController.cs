using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiodeBackEndFinal.DAL;
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
            homeVM.Categories = _context.Categories.Where(c=>c.IsDisable==false).OrderByDescending(c => c.Products.Count()).Take(4).ToList(); 
            return View(homeVM);
        }

    }
}
