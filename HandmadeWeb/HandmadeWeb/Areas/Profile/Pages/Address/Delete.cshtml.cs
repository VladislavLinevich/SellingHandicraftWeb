using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HandmadeWeb.Data;
using HandmadeWeb.Entities;

namespace HandmadeWeb.Areas.Profile.Pages.Address
{
    public class DeleteModel : PageModel
    {
        private readonly HandmadeWeb.Data.ApplicationDbContext _context;

        public DeleteModel(HandmadeWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public HandmadeWeb.Entities.Address Address { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Index");
            }
            var address = await _context.Address.FindAsync(id);

            if (address != null)
            {
                Address = address;
                _context.Address.Remove(Address);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
