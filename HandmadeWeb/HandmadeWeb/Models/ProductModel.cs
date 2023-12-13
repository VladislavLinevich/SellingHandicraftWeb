using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace HandmadeWeb.Models
{
    public class ProductModel
    {

        private readonly ApplicationDbContext _db;

        public ProductModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Category> GetSubcategories(int categoryId)
        {
            var subСategories = _db.Category.Where(c => c.ParentId == categoryId).ToList();
            var tempSubcategories = new List<Category>(subСategories);
            foreach (var category in tempSubcategories)
            {
                var children = GetSubcategories(category.Id);
                foreach (var child in children)
                {
                    subСategories.Add(child);
                }
            }
            return subСategories;
        }

        public List<Product> GetProductsByCategory(Category category)
        {
            /* var sub_categories = _db.Category.FromSqlRaw("WITH RECURSIVE r AS (" +
                 "SELECT \"Id\", \"ParentId\", \"Name\", \"Image\" " +
                 "FROM public.\"Category\"" +
                 $"WHERE \"ParentId\" = {category.Id}  " +
                 "UNION " +
                 "SELECT public.\"Category\".\"Id\", public.\"Category\".\"ParentId\", public.\"Category\".\"Name\", public.\"Category\".\"Image\"" +
                 "FROM public.\"Category\"  " +
                 "JOIN r  " +
                 "ON public.\"Category\".\"ParentId\" = r.\"Id\") SELECT * FROM r;").ToList();*/
            var sub_categories = GetSubcategories(category.Id);
            sub_categories.Add(category);

            return _db.Product.AsEnumerable().Where(p => sub_categories.Any(c => c.Id == p.CategoryId)).ToList();
        }

        public List<Product> GetProductsByCategory(Category category, int count)
        {
            var sub_categories = GetSubcategories(category.Id);
            sub_categories.Add(category);

            return _db.Product.AsEnumerable().Where(p => sub_categories.Any(c => c.Id == p.CategoryId)).Take(count).ToList();
        }
    }
}
