using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIZZA.Infrastructure;
using PIZZA.Models;
using System.Data;

namespace PIZZA.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductsController(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index(string categorySlug = "", int p = 1)
		{

			if (categorySlug == "")
			{
				return View(await _context.Products.OrderBy(p => p.Id)
				.Include(p => p.Category)
				.ToListAsync());
			}

			Category category = await _context.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();

			if (category == null) return RedirectToAction("Index");

			var productsByCategory = _context.Products.Where(p => p.CategoryId == category.Id);

			return View(await productsByCategory.OrderBy(p => p.Id).Skip((p - 1)).ToListAsync());
		}
	}
}
