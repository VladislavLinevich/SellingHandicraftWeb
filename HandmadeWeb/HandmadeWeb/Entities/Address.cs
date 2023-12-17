using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HandmadeWeb.Entities
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите полное имя")]
        [MaxLength(128)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Введите улицу")]
        [MaxLength(128)]
        public string Street { get; set; }

        [Required(ErrorMessage = "Введите город")]
        [MaxLength(128)]
        public string City { get; set; }

        [Required(ErrorMessage = "Введите почтовый индекс")]
        [MaxLength(128)]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Введите телефон")]
        [Phone(ErrorMessage = "Введите корректный телефон")]
        [RegularExpression(@"^\+375 \((17|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}", ErrorMessage = "Введите корректный телефон (+375 (xx) xxx-xx-xx)")]
        public string Phone { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
