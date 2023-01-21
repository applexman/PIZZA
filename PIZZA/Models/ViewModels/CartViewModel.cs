namespace PIZZA.Models.ViewModels
{
	public class CartViewModel
	{
		public List<CartItem> CartItems { get; set; }
		public decimal GrantTotal { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PostCode { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }

	}
}
