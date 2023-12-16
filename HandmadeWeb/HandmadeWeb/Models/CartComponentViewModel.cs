using HandmadeWeb.Entities;

namespace HandmadeWeb.Models
{
    public class CartComponentViewModel
    {
        public Cart cart { get; set; }
        public List<CartItem> cartItems { get; set; } = new List<CartItem>();
    }
}
