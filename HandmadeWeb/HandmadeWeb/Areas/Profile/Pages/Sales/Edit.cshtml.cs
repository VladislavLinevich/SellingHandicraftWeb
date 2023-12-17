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
using HandmadeWeb.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace HandmadeWeb.Areas.Profile.Pages.Sales
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly HandmadeWeb.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        IWebHostEnvironment _appEnvironment;
        private readonly ProductModel productModel;

        public EditModel(HandmadeWeb.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment appEnvironment)
        {
            _context = context; 
            _userManager = userManager;
            productModel = new ProductModel(context);
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Index");
            }

            var product =  await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return RedirectToPage("./Index");
            }

            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            if (product.UserId != authUser.Id)
            {
                return RedirectToPage("./Index");
            }

            Product = product;
            var allSubCategories = productModel.GetAllSubCategories();
            ViewData["CategoryId"] = new SelectList(allSubCategories, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile image)
        {
            var allSubCategories = productModel.GetAllSubCategories();
            ViewData["CategoryId"] = new SelectList(allSubCategories, "Id", "Name");

            ModelState.Remove("image");
            ModelState.Remove("Product.CartItems");
            ModelState.Remove("Product.OrderItems");
            ModelState.Remove("Product.Reviews");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Введите коректные данные");
                return Page();
            }

            if (image != null)
            {
                string delPath = _appEnvironment.WebRootPath + $"{Product.Image.Substring(1)}";
               
                string path = "/images/" + $"{Guid.NewGuid()}-{image.FileName}";
                var ext = Path.GetExtension(path);
                if (ext != ".jpg" && ext != ".png")
                {
                    ModelState.AddModelError("", "Изображения только PNG или JPG");
                    return Page();
                }

                if (System.IO.File.Exists(delPath))
                {
                    System.IO.File.Delete(delPath);
                }

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                Product.Image = "~" + path;
            }


            var authUser = await _userManager.GetUserAsync(HttpContext.User);
            Product.UserId = authUser.Id;

            _context.Attach(Product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
