using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace HandmadeWeb.Models
{
    public class ProductModel
    {

        private readonly ApplicationDbContext _db;

        public ProductModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Category> GetSubcategories(int categoryId)
        {
            var subСategories = _db.Category.Where(c => c.ParentId == categoryId).ToList();
            var tempSubcategories = new List<Category>(subСategories);
            foreach (var category in tempSubcategories)
            {
                var children = GetSubcategories(category.Id);
                foreach (var child in children)
                {
                    subСategories.Add(child);
                }
            }
            return subСategories;
        }

        public List<Product> GetProductsByCategory(Category category)
        {
            var sub_categories = GetSubcategories(category.Id);
            sub_categories.Add(category);

            return _db.Product.AsEnumerable().Where(p => sub_categories.Any(c => c.Id == p.CategoryId)).ToList();
        }

        public List<Product> GetProductsByCategory(Category category, int count)
        {
            var sub_categories = GetSubcategories(category.Id);
            sub_categories.Add(category);

            return _db.Product.AsEnumerable().Where(p => sub_categories.Any(c => c.Id == p.CategoryId)).Take(count).ToList();
        }

        public async Task<(bool, string)> AddToCart(Product product, Cart cart)
        {
            var cartItem = await _db.CartItem.FindAsync(cart.Id, product.Id);

            if (cartItem == null)
            {
                cartItem = new CartItem { CartId = cart.Id, ProductId = product.Id, Quantity = 1 };
                await _db.CartItem.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
                if (cartItem.Quantity > product.Quantity)
                {
                    return (false, "Вы добавили максимальное количество данного товара в корзину");
                }
            }

            await _db.SaveChangesAsync();

            return (true, "");
        }

        public ProductViewModel SetProductViewModel(int productId, ApplicationUser user)
        {
            var productViewModel = new ProductViewModel();

            var product = _db.Product.Find(productId);
            productViewModel.product = product;
            productViewModel.users = _db.Users.ToList();
            productViewModel.reviews = _db.Review.Where(r => r.ProductId == product.Id).ToList();
            if (user != null)
            {
                var userReview = productViewModel.reviews.FirstOrDefault(r => r.UserId == user.Id && r.ProductId == product.Id);
                if (userReview != null)
                {
                    productViewModel.review = userReview;
                }
            }

            return productViewModel;
        }
    }
}
