using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeWeb.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(512)]
        public string Avatar {  get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date_of_birth { get; set; }

        public List<Address> Addresses { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }
        public Cart Cart { get; set; }
    }
}
