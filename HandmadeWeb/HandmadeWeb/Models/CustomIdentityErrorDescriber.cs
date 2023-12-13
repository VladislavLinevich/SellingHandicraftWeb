using Microsoft.AspNetCore.Identity;

namespace HandmadeWeb.Models
{
    public class CustomIdentityErrorDescriber: IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string username)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = "Введенное имя пользователя уже существует"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = "Введенный адрес электронной почты уже существует"
            };
        }
    }
}
