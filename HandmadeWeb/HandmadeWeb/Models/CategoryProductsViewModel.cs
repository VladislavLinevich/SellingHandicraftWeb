using HandmadeWeb.Entities;

namespace HandmadeWeb.Models
{
    public class CategoryProductsViewModel
    {
        public List<Product> products { get; set; } = new List<Product>();
        public List<ApplicationUser> users { get; set; } = new List<ApplicationUser>();
        public List<Review> reviews { get; set; } = new List<Review>();
    }
}
