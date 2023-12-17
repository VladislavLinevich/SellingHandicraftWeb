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

namespace HandmadeWeb.Areas.Profile.Pages.Account
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser User { get;set; }
        public string Role { get; set; }

        public async Task OnGetAsync()
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            var roleName = await _userManager.GetRolesAsync(authUser);
            if (roleName[0] == "user")
            {
                Role = "Посетитель";
            }
            else
            {
                Role = "Продавец";
            }
            User = authUser;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            await _userManager.RemoveFromRoleAsync(authUser, "user");
            await _userManager.AddToRoleAsync(authUser, "seller");

            return RedirectToPage("./Index");
        }
    }
}
