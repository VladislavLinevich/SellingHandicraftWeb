using HandmadeWeb.Entities;

namespace HandmadeWeb.Models
{
    public class HomeViewModel
    {
        public Dictionary<string, List<Product>> products { get; set; } = new Dictionary<string, List<Product>>();
        public List<ApplicationUser> users { get; set; } = new List<ApplicationUser>();
        public List<Review> reviews { get; set; } = new List<Review>();
    }
}
