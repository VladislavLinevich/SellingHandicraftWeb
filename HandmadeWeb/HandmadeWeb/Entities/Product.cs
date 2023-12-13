using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeWeb.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(512)]
        public string Description { get; set; }

        [Required]
        [Precision(6, 2)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(512)]
        public string Image { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int NumberOfSales { get; set; } = 0;

        [Required]
        public bool IsDeleted {  get; set; } = false;

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public List<Review> Reviews { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
