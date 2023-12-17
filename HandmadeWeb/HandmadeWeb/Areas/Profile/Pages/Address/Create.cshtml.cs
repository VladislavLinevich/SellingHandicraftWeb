using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HandmadeWeb.Areas.Profile.Pages.Address
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly HandmadeWeb.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(HandmadeWeb.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public HandmadeWeb.Entities.Address Address { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Address.User");
            ModelState.Remove("Address.UserId");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Введите коректные данные");
                return Page();
            }


            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            Address.UserId = authUser.Id;
            _context.Address.Add(Address);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
