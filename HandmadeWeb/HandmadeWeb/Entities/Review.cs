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

        [Required]
        [MaxLength(512)]
        public string Text { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ReviewDate { get; set; }
    }
}
