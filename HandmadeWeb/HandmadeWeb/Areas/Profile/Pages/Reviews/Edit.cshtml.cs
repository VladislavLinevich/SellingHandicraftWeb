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

namespace HandmadeWeb.Areas.Profile.Pages.Reviews
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
        public Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null)
            {
                return RedirectToPage("./Index");
            }

            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            var review = await _context.Review.FindAsync(authUser.Id, productId);
            if (review == null)
            {
                return RedirectToPage("./Index");
            }
            Review = review;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Review.User");
            ModelState.Remove("Review.UserId");
            ModelState.Remove("Review.Products");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Введите коректные данные");
                return Page();
            }

            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            Review.UserId = authUser.Id;
            Review.ReviewDate = DateTime.Now;

            _context.Attach(Review).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
