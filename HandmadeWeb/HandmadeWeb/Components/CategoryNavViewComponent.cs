using HandmadeWeb.Data;
using HandmadeWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeWeb.Components
{
    public class CategoryNavViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CategoryNavViewComponent(ApplicationDbContext context)
        {
            _context = context;

        }
        public IViewComponentResult Invoke()
        {
            var mainCategories = _context.Category.Where(c => c.ParentId == null).ToList();
            return View(mainCategories);
        }
    }
}
