using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeWeb.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [StringLength(32, ErrorMessage = "Максимальная длина имени пользователя равна {1} символам.")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Ведите корректный адрес электронной почты")]
        [StringLength(64, ErrorMessage = "Максимальная длина электронной почты равна {1} символам")]
        public override string Email { get; set; }

        [MaxLength(512)]
        public string Avatar {  get; set; }

        [Required(ErrorMessage = "Укажите дату рождения")]
        [Column(TypeName = "date")]
        public DateTime Date_of_birth { get; set; }

        public List<Address> Addresses { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }
        public Cart Cart { get; set; }
    }
}
