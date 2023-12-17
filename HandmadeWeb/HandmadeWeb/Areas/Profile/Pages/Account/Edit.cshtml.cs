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
using System.IO;

namespace HandmadeWeb.Areas.Profile.Pages.Account
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly HandmadeWeb.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        IWebHostEnvironment _appEnvironment;

        public EditModel(HandmadeWeb.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public ApplicationUser User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("./Index");
            }
            User = user;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile image)
        {
            ModelState.Remove("image");
            ModelState.Remove("User.Cart");
            ModelState.Remove("User.Avatar");
            ModelState.Remove("User.Products");
            ModelState.Remove("User.Date_of_birth");
            ModelState.Remove("User.Reviews");
            ModelState.Remove("User.Orders");
            ModelState.Remove("User.Addresses");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Введите коректные данные");
                return Page();
            }


            string delPath = _appEnvironment.WebRootPath + $"{User.Avatar.Substring(1)}";
            string lastPath = User.Avatar;
            if (image != null)
            {
                string path = "/images/" + $"{User.Id}-{image.FileName}";
                var ext = Path.GetExtension(path);
                if (ext != ".jpg" && ext != ".png")
                {
                    ModelState.AddModelError("", "Изображения только PNG или JPG");
                    return Page();
                }

                User.Avatar = "~" + path;
            }

            var editUser = await _userManager.GetUserAsync(HttpContext.User);

            editUser.UserName = User.UserName;
            editUser.Email = User.Email;
            editUser.Avatar = User.Avatar;
            var result = await _userManager.UpdateAsync(editUser);

            if (result.Succeeded)
            {
                if (image != null)
                {
                    string path = "/images/" + $"{User.Id}-{image.FileName}";
                    if (System.IO.File.Exists(delPath) && lastPath != "~/images/Avatar.png")
                    {
                        System.IO.File.Delete(delPath);
                    }
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
                
            return RedirectToPage("./Index");
        }
    }
}
