using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIZZA.Models;
using PIZZA.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace PIZZA.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int p = 1)
        {
            return View(await _context.Products.OrderBy(p => p.Id)
                .Include(p => p.Category)
                .ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "_");

                var slug = await _context.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
                if (slug != null)
                {
                    TempData["Error"] = "Produkt już istnieje w bazie!";
                    return View(product);
                }

                product.Image = "soon.png";

                _context.Add(product);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Nie udało się dodać produktu. Spróbuj ponownie później.");
                }

                    return RedirectToAction("Index");

            }

            return View(product);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _context.Products.FindAsync(id);

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
                if (slug != null)
                {
                    TempData["Error"] = "Produkt już istnieje w bazie!";
                    return View(product);
                }
                _context.Update(product);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Produkt został pomyślnie zmieniony!";
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Produkt został pomyślnie usunięty!";

            return RedirectToAction("Index");
        }
    }
}
