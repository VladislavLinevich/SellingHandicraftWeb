using HandmadeWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace HandmadeWeb.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(512)]
        public string Image { get; set; }

        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public List<Category> Children { get; set; }

        public List<Product> Products { get; set; }
    }
}
