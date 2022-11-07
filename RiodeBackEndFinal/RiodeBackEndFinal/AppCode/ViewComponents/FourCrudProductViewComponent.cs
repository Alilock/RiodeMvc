using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiodeBackEndFinal.DAL;

namespace RiodeBackEndFinal.AppCode.ViewComponents
{
    public class FourCrudProductViewComponent :ViewComponent
    {
        public FourCrudProductViewComponent(RiodeContext context)
        {
            Context = context;
        }

        public RiodeContext Context { get; }
        public IViewComponentResult Invoke()
        {
            var r = new Random();
            var products = Context.Products.Include(p => p.ProductImages).Include(p => p.ProductBadges).ThenInclude(pb => pb.Badge);
            var lengtg= products.Count();
            var randomprods= products.OrderBy(p=>p.Name).Take(12).ToList();

            return View(randomprods);
        }

    }
}
