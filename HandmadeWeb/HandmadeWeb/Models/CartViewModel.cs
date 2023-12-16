using HandmadeWeb.Entities;

namespace HandmadeWeb.Models
{
    public class CartViewModel
    {   
        public Dictionary<ApplicationUser, List<CartItem>> cartItems { get; set; } = new Dictionary<ApplicationUser, List<CartItem>>();
        public List<Product> products { get; set; } = new List<Product>();
    }
}
