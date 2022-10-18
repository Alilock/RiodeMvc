using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiodeBackEndFinal.DAL;
using RiodeBackEndFinal.Models;
using RiodeBackEndFinal.Utlis.Extensions;

namespace RiodeBackEndFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        public RiodeContext _context { get; }

        public SliderController(RiodeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (slider.Title == null)
            {
                ModelState.AddModelError("Name", "Title daxil edin");
                return  View();
            }
            if ( slider== null) return BadRequest();
            if (slider.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Zəhmət olmasa faylı seçin");
                return View();
            }
            var image = slider.ImageFile;
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
            
            string newfilename = Guid.NewGuid().ToString();
            newfilename += image.CutFileName();
            slider.ImageName = newfilename;
            image.SaveFile(Path.Combine("Assets", "Images", newfilename));
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();
            Slider slider = _context.Sliders.Where(c => c.Id == id).FirstOrDefault();
            if (slider is null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        public IActionResult Edit(int id, Slider slider)
        {
            Slider item = _context.Sliders.Find(id);
            if (item is null) return NotFound();
            if (slider.ImageFile != null)
            {
                IFormFile file = slider.ImageFile;
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
                RemoveFile(Path.Combine("Assets", "Images", item.ImageName));
                file.SaveFile(Path.Combine("Assets", "Images", newFileName));
                item.ImageName = newFileName;
            }
            item.Title = slider.Title;
            item.SubTitle = slider.SubTitle;
            item.IsLeftSide = slider.IsLeftSide;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var slider = _context.Sliders.Find(id);
            RemoveFile(Path.Combine("Assets", "Images", slider.ImageName));
            _context.Remove(slider);
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
                        error= true,
                        message= "taplmadi"
                      };
                    return Json(response);
                }
            var slider = _context.Sliders.Find(id);
            if (slider is null)
                {
                    var response = new
                    {
                        error = true,
                        message = "taplmadi"
                    };
                    return Json(response);
                }
            if (slider.IsDisable==false)
                {
                    slider.IsDisable= true;
                }
            else
                {
                    slider.IsDisable = false;
                }
            _context.SaveChanges();
            return Json(new {
                error=false,
                message="ok"
            });

        }
        public static void RemoveFile(string path)
        {
            path = Path.Combine(RiodeBackEndFinal.Utlis.Constant.Constant.RoothPath, path);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

    }
}
