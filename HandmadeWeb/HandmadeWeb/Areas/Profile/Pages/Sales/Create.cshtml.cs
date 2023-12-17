using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using HandmadeWeb.Models;
using System.Data;

namespace HandmadeWeb.Areas.Profile.Pages.Sales
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly HandmadeWeb.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        IWebHostEnvironment _appEnvironment;
        private readonly ProductModel productModel;

        public CreateModel(HandmadeWeb.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userManager;
            productModel = new ProductModel(context);
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            var roleName = await _userManager.GetRolesAsync(authUser);
            if (roleName[0] == "user")
            {
                TempData["RoleError"] = "Вы должны стать продавцом. Перейдите в профиле в свой аккаунт и нажмите \"Начать продавать\"";
                return RedirectToPage("./Index");
            }
            var allSubCategories = productModel.GetAllSubCategories();
            ViewData["CategoryId"] = new SelectList(allSubCategories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile image)
        {
            var allSubCategories = productModel.GetAllSubCategories();
            ViewData["CategoryId"] = new SelectList(allSubCategories, "Id", "Name");

            ModelState.Remove("image");
            ModelState.Remove("Product.User");
            ModelState.Remove("Product.UserId");
            ModelState.Remove("Product.Image");
            ModelState.Remove("Product.Reviews");
            ModelState.Remove("Product.CartItems");
            ModelState.Remove("Product.OrderItems");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Введите коректные данные");
                return Page();
            }

            if (image != null)
            {
                string path = "/images/" + $"{Guid.NewGuid()}-{image.FileName}";
                var ext = Path.GetExtension(path);
                if (ext != ".jpg" && ext != ".png")
                {
                    ModelState.AddModelError("", "Изображения только PNG или JPG");
                    return Page();
                }
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                Product.Image = "~" + path;
            }
            else
            {
                ModelState.AddModelError("", "Укажите изображение");
                return Page();
            }

            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            Product.UserId = authUser.Id;
            Product.IsDeleted = false;
            _context.Product.Add(Product);           

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
