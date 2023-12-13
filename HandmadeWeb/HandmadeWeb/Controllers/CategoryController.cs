using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using HandmadeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandmadeWeb.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext dbContext;
        private ProductModel productModel;
        private CategoryProductsViewModel viewModel;
        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
            productModel = new ProductModel(applicationDbContext);
            viewModel = new CategoryProductsViewModel();
        }

        public IActionResult Index(int? category)
        {
            var currentCategory = category ?? 0;
            var categoryName = currentCategory != 0
                        ? dbContext.Category.FirstOrDefault(c => c.Id == currentCategory)?.Name
                        : "Все категории";

            var sub_categories = dbContext.Category.Where(c => c.ParentId == category).ToList();
            ViewData["SubCategories"] = sub_categories;
            ViewData["CategoryName"] = categoryName;


            List<Product> subProducts;
            if (currentCategory != 0)
            {
                var curCategory = dbContext.Category.FirstOrDefault(c => c.Id == currentCategory);
                if (curCategory == null)
                {
                    return RedirectToAction("Index", "Category");
                }
                subProducts = productModel.GetProductsByCategory(curCategory);
            } else
            {
                subProducts = dbContext.Product.ToList();
            } 


            viewModel.products = subProducts;
            viewModel.users = dbContext.Users.ToList();
            viewModel.reviews = dbContext.Review.ToList();
            return View(viewModel);
        }
    }
}
