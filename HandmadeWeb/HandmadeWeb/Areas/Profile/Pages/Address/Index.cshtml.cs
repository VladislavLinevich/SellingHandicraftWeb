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

namespace HandmadeWeb.Areas.Profile.Pages.Address
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

        public List<HandmadeWeb.Entities.Address> Address { get;set; }

        public async Task OnGetAsync()
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            Address = await _context.Address
            .Where(a => a.UserId == authUser.Id).ToListAsync();
            
        }
    }
}
