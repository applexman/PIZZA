using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIZZA.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Wpisz nazwę")]
        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Musisz podać cenę produktu")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Wybierz kategorię")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Image { get; set; } = "soon.png";

    }
}
