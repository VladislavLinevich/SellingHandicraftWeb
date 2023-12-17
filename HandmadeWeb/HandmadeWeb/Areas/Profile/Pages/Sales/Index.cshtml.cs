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

namespace HandmadeWeb.Areas.Profile.Pages.Sales
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

        public List<Product> Products { get;set; }

        public async Task OnGetAsync()
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            Products = await _context.Product
            .Where(p => p.UserId == authUser.Id).ToListAsync();
        }
    }
}
