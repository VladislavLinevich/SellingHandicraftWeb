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

namespace HandmadeWeb.Areas.Profile.Pages.Reviews
{
    public class DeleteModel : PageModel
    {
        private readonly HandmadeWeb.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(HandmadeWeb.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Review Review { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync(int productId)
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            var review = await _context.Review.FindAsync(authUser.Id, productId);

            if (review != null)
            {
                Review = review;
                _context.Review.Remove(Review);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
