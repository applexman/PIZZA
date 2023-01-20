using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIZZA.Models
{
    public class Order
    {
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderNumber { get; set; } = Guid.NewGuid().ToString();
        public decimal Total { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser IdentityUser { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }

    public enum OrderStatus
    {
        Zlecone,
        Tworzone,
        Transportowane,
        Dostarczone,
        Anulowane
    }
}
