using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using HandmadeWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HandmadeWeb.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext dbContext;
        private UserManager<ApplicationUser> _userManager;
        private CartViewModel _cartViewModel;

        public CartController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            dbContext = applicationDbContext;
            _userManager = userManager;
            _cartViewModel = new CartViewModel();
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            var cart = await dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == authUser.Id);

            var TotalCount = dbContext.CartItem.Where(c => c.CartId == cart.Id).Count();
            ViewData["CartCount"] = TotalCount;

            var query =
                    from product in dbContext.Product
                    join cartItem in dbContext.CartItem on product.Id equals cartItem.ProductId
                    join user in dbContext.Users on product.UserId equals user.Id
                    where cartItem.CartId == cart.Id
                    group new { CartItem = cartItem, Seller = user } by user into grouped
                    select new
                    {
                        Seller = grouped.Key,
                        CartItems = grouped.Select(g => g.CartItem).ToList()
                    };

            var cartItems = query.ToDictionary(item => item.Seller, item => item.CartItems);

            _cartViewModel.cartItems = cartItems;
            _cartViewModel.products = dbContext.Product.ToList();
            return View(_cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int itemId)
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            var cart = await dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == authUser.Id);

            var cartItem = await dbContext.CartItem.FindAsync(cart.Id, itemId);

            cartItem.Quantity += 1;

            await dbContext.SaveChangesAsync(); 

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reduce(int itemId)
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            var cart = await dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == authUser.Id);

            var cartItem = await dbContext.CartItem.FindAsync(cart.Id, itemId);

            cartItem.Quantity -= 1;

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int itemId)
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            var cart = await dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == authUser.Id);

            var cartItem = await dbContext.CartItem.FindAsync(cart.Id, itemId);
            dbContext.CartItem.Remove(cartItem);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
