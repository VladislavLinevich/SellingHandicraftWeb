using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeWeb.Entities
{

    [PrimaryKey(nameof(UserId), nameof(ProductId))]
    public class Review
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Введите текст отзыва")]
        [MaxLength(512)]
        [StringLength(512, ErrorMessage = "Длина отзыва минимум {2} символа", MinimumLength = 32)]
        public string Text { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ReviewDate { get; set; }
    }
}
