using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiodeBackEndFinal.DAL;
using RiodeBackEndFinal.Models;
using RiodeBackEndFinal.Utlis.Extensions;
namespace RiodeBackEndFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        RiodeContext _context { get;}
        public ProductController(RiodeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.ProductImages).ToList();
            ICollection<Product> productss = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductBadges)
                .ThenInclude(p => p.Badge).ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            ViewBag.Badges = _context.Badges;
            ViewBag.Colors = _context.Colors;
            ViewBag.Categories = _context.Categories;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ViewBag.Badges = _context.Badges;
            ViewBag.Colors = _context.Colors;
            ViewBag.Categories = _context.Categories;
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (product.MainImg is null)
            {
                ModelState.AddModelError("MainImg", "Zəhmət olmasa şəkil seçin");
                return View();
            }
            if (product.SellPrice < product.CostPrice)
            {
                ModelState.AddModelError("SellPrice", "Satış qiyməti maya dəyərindən kiçik ola bilməz!");
            }
            #region Image
            //other images
            var productImgs = product.OtherImgs;
            List<ProductImages> images = new List<ProductImages>();
            if (productImgs != null)
            {
                foreach (var image in productImgs)
                {
                    if (!image.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("MainImage", "Yüklədiyiniz fayl şəkil deyil");
                        return View();
                    }
                    if (!image.CheckFileSize(2))
                    {
                        ModelState.AddModelError("MainImage", "Yüklədiyiniz şəkil 2mb-dan artıq olmamalıdır");
                        return View();
                    }
                }

                foreach (var image in productImgs)
                {
                    string imagename = Guid.NewGuid() + image.CutFileName();
                    image.SaveFile(Path.Combine("assets", "images", imagename));
                    images.Add(new()
                    {
                        ImageName = imagename,
                        Product = product,
                        IsMain = null
                    });

                }
            }
            //main image
            var mainimg = product.MainImg;
            if (!mainimg.CheckFileType("image/"))
            {
                ModelState.AddModelError("MainImage", "Yüklədiyiniz fayl şəkil deyil");
                return View();
            }
            if (!mainimg.CheckFileSize(2))
            {
                ModelState.AddModelError("MainImage", "File is too big");
                return View();
            }
            string mainImgName = Guid.NewGuid() + mainimg.CutFileName();
            mainimg.SaveFile(Path.Combine("assets", "images", mainImgName));
            images.Add(new()
            {
                ImageName = mainImgName,
                IsMain = true,
                Product = product
            });
            // hover image
            var hoverImg = product.HoverImg;
            if (hoverImg != null)
            {
                if (!hoverImg.CheckFileType("image/"))
                {
                    ModelState.AddModelError("MainImage", "Yüklədiyiniz fayl şəkil deyil");
                    return View();
                }
                if (!hoverImg.CheckFileSize(2))
                {
                    ModelState.AddModelError("MainImage", "File is too big");
                    return View();
                }
                string hoverImgName = Guid.NewGuid() + hoverImg.CutFileName();
                hoverImg.SaveFile(Path.Combine("assets", "images", hoverImgName));
                images.Add(new()
                {
                    ImageName = hoverImgName,
                    IsMain = false,
                    Product = product
                });

            }
            #endregion

            product.ProductImages = images;
            if (product.ColorIds != null)
            {
                product.ProductColors = new List<ProductColors>();
                foreach (var item in product.ColorIds)
                {
                    product.ProductColors.Add(new()
                    {
                        ColorId = item,
                        Product = product
                    });
                }
            }
            if (product.BadgeIds != null)
            {
                product.ProductBadges = new List<ProductBadges>();
                foreach (var item in product.BadgeIds)
                {
                    product.ProductBadges.Add(new()
                    {
                        BadgeId = item,
                        Product = product
                    });
                }
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public  IActionResult Edit(int? id)
        {
            ViewBag.Images = _context.ProductImages.ToList();
            ViewBag.Badges = _context.Badges;
            ViewBag.Colors = _context.Colors;
            ViewBag.Categories = _context.Categories;
            if (id is null) return BadRequest();
            var prod = _context.Products.Where(p => p.Id == id)
                                        .Include(p => p.ProductImages)
                                        .Include(p => p.ProductBadges)
                                        .ThenInclude(p=>p.Badge)
                                        .SingleOrDefault();
    
            if (prod is null) return NotFound();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var edited = _context.Products.Find(product);
            product.Name= edited.Name;
            product.Brand = edited.Brand;
            product.Description= edited.Description;    
            product.CategoryId= edited.CategoryId;
            product.BadgeIds= edited.BadgeIds;
            product.CostPrice = edited.CostPrice;
            product.SellPrice= edited.SellPrice;
            product.DiscountPercent=edited.DiscountPercent; 
            product.ColorIds= edited.ColorIds;
            List<IFormFile> newImages = product.OtherImgs;
            List<ProductImages> images = new List<ProductImages>();
            if (newImages !=null)
            {
                foreach (var image in newImages)
                {
                    if (!image.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("MainImage", "Yüklədiyiniz fayl şəkil deyil");
                        return View();
                    }
                    if (!image.CheckFileSize(2))
                    {
                        ModelState.AddModelError("MainImage", "Yüklədiyiniz şəkil 2mb-dan artıq olmamalıdır");
                        return View();
                    }
                    foreach (var img in newImages)
                    {
                        string imgUrl = Guid.NewGuid() + img.CutFileName();
                        img.SaveFile(Path.Combine("assets", "images", imgUrl));
                        images.Add(new()
                        {
                            ImageName = imgUrl,
                            IsMain = null,
                            Product = edited,
                        });
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));  
        }
    }
}
