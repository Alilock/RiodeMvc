using Microsoft.AspNetCore.Mvc;
using RiodeBackEndFinal.DAL;
using RiodeBackEndFinal.Models;

namespace RiodeBackEndFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BadgeController : Controller
    {
        RiodeContext _context { get; set; }
        public BadgeController(RiodeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Badges);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Badge badge)
        {
            if (String.IsNullOrWhiteSpace(badge.Name))
            {
                ModelState.AddModelError("Name", "Adi daxil edin");
                return View();
            }
            _context.Badges.Add(badge);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();
            var deleted = _context.Badges.Where(b => b.Id == id).FirstOrDefault();
            if (deleted is null) return NotFound();
            _context.Badges.Remove(deleted);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();
            var edited = _context.Badges.Where(b => b.Id == id).FirstOrDefault();
            if (edited is null) return NotFound();
            return View(edited);
        }
        [HttpPost]
        public IActionResult Edit(int id, Badge badge)
        {
            Badge edited = _context.Badges.Where(b => b.Id == id).FirstOrDefault();
            if (edited is null) return NotFound();
            if (String.IsNullOrWhiteSpace(badge.Name))
            {
                ModelState.AddModelError("Name", "Adi daxil edin");
                return View();
            }
            edited.Name=badge.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
