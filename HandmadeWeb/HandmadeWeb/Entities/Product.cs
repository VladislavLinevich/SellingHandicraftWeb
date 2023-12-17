using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeWeb.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название продукта")]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание продукта")]
        [MaxLength(512)]
        [StringLength(512, ErrorMessage = "Описание должно быть не менее {2} символов", MinimumLength = 32)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите сумму для открытия счета")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Введите корректную сумму (Максимум 2 знака после запятой, например 200,45)")]
        [Range(1, 100000.00, ErrorMessage = "Максимальная сумма от 1 до 100000.00")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Укажите изображение")]
        [MaxLength(512)]
        public string Image { get; set; }

        [Required(ErrorMessage = "Введите количество товара")]
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
