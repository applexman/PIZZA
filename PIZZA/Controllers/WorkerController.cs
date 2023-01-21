using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIZZA.Infrastructure;

namespace PIZZA.Controllers
{
	[Authorize(Roles = "Admin, Employee")]
	public class WorkerController : Controller
	{
		private readonly ApplicationDbContext _context;

		public WorkerController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Worker
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Orders
				.Include(o => o.IdentityUser)
				.Include(o => o.CartItems);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Worker/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Orders == null)
			{
				return NotFound();
			}

			var order = await _context.Orders
				.Include(o => o.IdentityUser)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (order == null)
			{
				return NotFound();
			}

			return View(order);
		}

		// POST: Worker/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Orders == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
			}
			var order = await _context.Orders.FindAsync(id);
			if (order != null)
			{
				_context.Orders.Remove(order);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool OrderExists(int id)
		{
			return _context.Orders.Any(e => e.Id == id);
		}
	}
}
