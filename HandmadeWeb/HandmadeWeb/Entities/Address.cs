using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HandmadeWeb.Entities
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Fullname { get; set; }

        [Required]
        [MaxLength(128)]
        public string Street { get; set; }

        [Required]
        [MaxLength(128)]
        public string City { get; set; }

        [Required]
        [MaxLength(128)]
        public string Postcode { get; set; }

        [Required]
        [MaxLength(128)]
        [Phone]
        public string Phone { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
