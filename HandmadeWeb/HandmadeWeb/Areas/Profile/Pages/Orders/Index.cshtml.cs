using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HandmadeWeb.Areas.Profile.Pages.Orders
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly HandmadeWeb.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(HandmadeWeb.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Dictionary<Order, List<OrderItem>> Orders { get; set; } = new Dictionary<Order, List<OrderItem>>();
        public List<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Product.ToListAsync();
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            var userOrders = await _context.Orders
            .Where(o => o.CustomerId == authUser.Id).ToListAsync();

            foreach (var order in userOrders)
            {
                var orderItems = await _context.OrderItem.Where(i => i.OrderId == order.Id).ToListAsync();
                Orders.Add(order, orderItems);
            }
        }
    }
}
