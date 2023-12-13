using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HandmadeWeb.Entities
{
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Orders { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
