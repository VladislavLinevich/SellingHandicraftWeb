using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HandmadeWeb.Areas.Profile.Pages.Reviews
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

        public List<Review> Review { get;set; }

        public List<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            Review = await _context.Review
            .Where(r => r.UserId == authUser.Id).ToListAsync();

            Products = await _context.Product.ToListAsync();
        }
    }
}
