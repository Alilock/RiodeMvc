using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;
using RiodeBackEndFinal.Areas.Admin.ViewModels;
using RiodeBackEndFinal.DAL;
using RiodeBackEndFinal.Models;
using RiodeBackEndFinal.Utlis.Extensions;

namespace RiodeBackEndFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        RiodeContext _context { get; set; }
        public CategoryController(RiodeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Categories= _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            ViewBag.Categories = _context.Categories.ToList();

            if (category.Name==null)
            {
                ModelState.AddModelError("Name", "Adi daxil edin");
                return View();
            }
            if (category == null) return BadRequest();
            if (category.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Zəhmət olmasa faylı seçin");
                return View();
            }
                var image = category.ImageFile;
            if (!image.CheckFileType("image/"))
            {
                ModelState.AddModelError("ImageFile", "Choose only image");
                return View();

            }
            if (!image.CheckFileSize(20))
            {
                ModelState.AddModelError("ImageFile", "File too big");
                return View();

            }
            string newfilename= Guid.NewGuid().ToString();
            newfilename+= image.CutFileName();
            category.ImageName = newfilename;
            image.SaveFile(Path.Combine("Assets", "Images",newfilename));
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            }
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = _context.Categories.ToList();

            if (id is null) return BadRequest();
            Category category= _context.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category is null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(int? id,Category category)
        {
            ViewBag.Categories = _context.Categories.ToList();

            if (id != category.Id || id is null) return BadRequest();
            Category item = _context.Categories.Find(id);
            if (item is null) return NotFound();
            if (category.ImageFile!=null)
            {
                IFormFile file = category.ImageFile;
                if (!file.CheckFileType("image/"))
                {
                    ModelState.AddModelError("ImageFile", "Yüklədiyiniz fayl şəkil deyil");
                    return View();
                }
                if (file.CheckFileSize(20))
                {
                    ModelState.AddModelError("ImageFile", "Yüklədiyiniz fayl 2mb-dan artıq olmamalıdır");
                    return View();
                }
            string newFileName = Guid.NewGuid().ToString();
            newFileName += file.CutFileName();
            RemoveFile(Path.Combine("Assets","Images", item.ImageName));
            file.SaveFile(Path.Combine("Assets", "Images", newFileName));
            item.ImageName = newFileName;
            }
            item.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var category = _context.Categories.Find(id);
            RemoveFile(Path.Combine("Assets","Images",category.ImageName));
            _context.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult SwitchStatus(int? id)
        {
            if (id is null)
            {
                var response = new
                {
                    error = true,
                    message = "taplmadi"
                };
                return Json(response);
            }
            var category = _context.Categories.Find(id);
            if (category is null)
            {
                var response = new
                {
                    error = true,
                    message = "taplmadi"
                };
                return Json(response);
            }
            if (category.IsDisable == false)
            {
                category.IsDisable = true;
            }
            else
            {
                category.IsDisable = false;
            }
            _context.SaveChanges();
            return Json(new
            {
                error = false,
                message = "ok"
            });

        }
        public static void RemoveFile(string path)
        {
            path = Path.Combine(RiodeBackEndFinal.Utlis.Constant.Constant.RoothPath,  path);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        
    }

}

