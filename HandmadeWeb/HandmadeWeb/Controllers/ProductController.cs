using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using HandmadeWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HandmadeWeb.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext dbContext;
        UserManager<ApplicationUser> _userManager;
        ProductModel ProductModel;
        public ProductController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            dbContext = applicationDbContext;
            _userManager = userManager;
            ProductModel = new ProductModel(applicationDbContext);
        }

        public async Task<IActionResult> Index(int productId)
        {
            /*var user2 = await _userManager.FindByIdAsync("3194e9cb-0b70-4553-8d8f-3c08f4049c94");

            if (user2 != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user2);
                var result = await _userManager.ResetPasswordAsync(user2, token, "123456");
            }*/
            var product = dbContext.Product.Find(productId);
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var viewModel = ProductModel.SetProductViewModel(productId, user);
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendReview(int productId, Review review)
        {
            ModelState.Remove("review.User");
            ModelState.Remove("review.UserId");
            ModelState.Remove("review.Products");
            if (ModelState.IsValid)
            {
                if (!User.Identity.IsAuthenticated)
                {                 
                    return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = $"/Product?productId={productId}" }); ;
                }
                var user = await _userManager.GetUserAsync(HttpContext.User);
                review.ProductId = productId;
                review.UserId = user.Id;
                review.ReviewDate = DateTime.Now;

                await dbContext.Review.AddAsync(review);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index", new { productId = productId });
            }
            else
            {
                ModelState.AddModelError("", "Error");
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var viewModel = ProductModel.SetProductViewModel(productId, user);
                return View("Index", viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeReview(int productId, Review review)
        {
            ModelState.Remove("review.User");
            ModelState.Remove("review.UserId");
            ModelState.Remove("review.Products");
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var changeReview = await dbContext.Review.FindAsync(user.Id, productId);
                changeReview.ReviewDate = DateTime.Now;
                changeReview.Rating = review.Rating;
                changeReview.Text = review.Text;

                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index", new { productId = productId });
            }
            else
            {
                ModelState.AddModelError("", "Error");
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var viewModel = ProductModel.SetProductViewModel(productId, user);
                return View("Index", viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            if (ModelState.IsValid)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = $"/Product?productId={productId}" }); ;
                }
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var cart = await dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == user.Id);
                var product = await dbContext.Product.FindAsync(productId);

                var (added, errorMessage) = await ProductModel.AddToCart(product, cart);

                if (!added)
                {
                    TempData["CartError"] = errorMessage;
                }
            }
            else
            {
                TempData["CartError"] = "Error";
            }

            return RedirectToAction("Index", new { productId = productId });
        }

    }
}
