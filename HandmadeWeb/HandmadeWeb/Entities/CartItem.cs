using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HandmadeWeb.Entities
{
    [PrimaryKey(nameof(CartId), nameof(ProductId))]
    public class CartItem
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
