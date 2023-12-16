using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using HandmadeWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandmadeWeb.Components
{
    public class CartViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;/*
        private CartComponentViewModel _cartViewModel;*/

        public CartViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;/*
            _cartViewModel = new CartComponentViewModel();*/
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var cart = await _context.Cart.FirstOrDefaultAsync(c => c.UserId == user.Id);

            var cartItems = _context.CartItem.Where(i => i.CartId == cart.Id).ToList();

           /*_cartViewModel.cart = cart;
           _cartViewModel.cartItems = cartItems;*/
            return View(cartItems);
        }
    }
}
