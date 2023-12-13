using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using HandmadeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandmadeWeb.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dbContext;
        private HomeViewModel viewModel;
        private ProductModel productModel;
        public HomeController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
            viewModel = new HomeViewModel();
            productModel = new ProductModel(applicationDbContext);
        }
        public IActionResult Index()
        {

            Dictionary<string, List<Product>> products = new Dictionary<string, List<Product>>();
            var mainCategories = dbContext.Category.Where(c => c.ParentId == null).ToList();
            ViewData["MainCategories"] = mainCategories;

            foreach (var category in mainCategories)
            {
                var subProducts = productModel.GetProductsByCategory(category, 10);

                products.Add(category.Name, subProducts);
            }

            viewModel.products = products;
            viewModel.users = dbContext.Users.ToList();
            viewModel.reviews = dbContext.Review.ToList();

            return View(viewModel);
        }
    }
}
