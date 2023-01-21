using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PIZZA.Models;

namespace PIZZA.Infrastructure
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId);
			modelBuilder.Entity<CartItem>()
				.HasOne(ci => ci.Order)
				.WithMany(o => o.CartItems)
				.HasForeignKey(ci => ci.OrderId);
			modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
			modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.RoleId, r.UserId });
			base.OnModelCreating(modelBuilder);
		}
	}
}
