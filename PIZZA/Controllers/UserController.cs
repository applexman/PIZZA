using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIZZA.Infrastructure;
using System.Security.Claims;

namespace PIZZA.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _context;

		public UserController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: User
		public async Task<IActionResult> Index()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var applicationDbContext = _context.Orders
				.Where(o => o.UserId == userId)
				.Include(o => o.IdentityUser)
				.Include(o => o.CartItems);
			if (applicationDbContext.Count() == 0)
			{
				TempData["Error"] = "Nie masz żadnych zamówień.";
				return RedirectToAction("Index", "Home");

			}
			else
			{
				return View(await applicationDbContext.ToListAsync());
			}
		}
	}
}
