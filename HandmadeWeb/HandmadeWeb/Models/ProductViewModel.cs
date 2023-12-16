using HandmadeWeb.Entities;

namespace HandmadeWeb.Models
{
    public class ProductViewModel
    {
        public Product product { get; set; }
        public List<ApplicationUser> users { get; set; } = new List<ApplicationUser>();
        public List<Review> reviews { get; set; } = new List<Review>();
        public Review review { get; set; }
    }
}
