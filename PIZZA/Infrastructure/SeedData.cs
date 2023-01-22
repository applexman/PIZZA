using Microsoft.EntityFrameworkCore;
using PIZZA.Models;

namespace PIZZA.Infrastructure
{
	public class SeedData
	{
		public static void SeedDatabase(ApplicationDbContext context)
		{
			context.Database.Migrate();

			if (!context.Products.Any())
			{

				Category pizza = new Category { Name = "Pizza", Slug = "pizza" };
				Category napoje = new Category { Name = "Napoje", Slug = "napoje" };

				context.Products.AddRange(
						new Product
						{
							Name = "MARGARITA",
							Description = "30 cm - SOS, SER",
							Price = 20.50M,
							Category = pizza,
							Image = "MARGARITA.jpg"
						},
						new Product
						{
							Name = "VESUVIO",
							Description = "30 cm - SOS, SER, SZYNKA",
							Price = 22.50M,
							Category = pizza,
							Image = "VESUVIO.jpg"
						},
						new Product
						{
							Name = "HAWAJSKA",
							Description = "30 cm - SOS, SER, SZYNKA, ANANAS",
							Price = 27.00M,
							Category = pizza,
							Image = "HAWAJSKA.jpg"
						},
						new Product
						{
							Name = "RZEŹNICKA",
							Description = "30 cm - SOS, SER, SZYNKA, BEKON, SALAMI, KABANOS, CEBULA",
							Price = 30.00M,
							Category = pizza,
							Image = "RZEŹNICKA.jpg"
						},
						new Product
						{
							Name = "Coca Cola",
							Description = "Coca Cola 0,85l",
							Price = 6.00M,
							Category = napoje,
							Image = "COLA.jpg"
						},
						new Product
						{
							Name = "Pepsi",
							Description = "Pepsi 0,85l",
							Price = 6.00M,
							Category = napoje,
							Image = "PEPSI.jpg"
						},
						new Product
						{
							Name = "Fanta",
							Description = "Fanta 0,85l",
							Price = 5.00M,
							Category = napoje,
							Image = "FANTA.jpg"
						},
						new Product
						{
							Name = "Nestea",
							Description = "Nestea 0,85l",
							Price = 4.00M,
							Category = napoje,
							Image = "NESTEA.jpg"
						}
				);

				context.SaveChanges();
			}
		}
	}
}
