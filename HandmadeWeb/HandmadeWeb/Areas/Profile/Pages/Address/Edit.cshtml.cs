using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HandmadeWeb.Areas.Profile.Pages.Address
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly HandmadeWeb.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(HandmadeWeb.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public HandmadeWeb.Entities.Address Address { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Index"); 
            }

            var address =  await _context.Address.FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return RedirectToPage("./Index");
            }

            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            if(address.UserId != authUser.Id)
            {
                return RedirectToPage("./Index");
            }
            Address = address;
            return Page();
        }

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
            _context.Attach(Address).State = EntityState.Modified;

            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
