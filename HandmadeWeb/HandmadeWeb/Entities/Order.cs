using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeWeb.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Fullname { get; set; }

        [Required]
        [MaxLength(128)]
        public string Street { get; set; }

        [Required]
        [MaxLength(128)]
        public string City { get; set; }

        [Required]
        [MaxLength(128)]
        public string Postcode { get; set; }

        [Required]
        [MaxLength(128)]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Precision(30, 2)]
        public decimal TotalCost { get; set; }

        public string CustomerId { get; set; }
        public ApplicationUser User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
