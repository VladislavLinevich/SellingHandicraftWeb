using HandmadeWeb.Data;
using HandmadeWeb.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;

namespace HandmadeWeb.Models
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<Category> categories = new List<Category>
            {
                new Category { Name = "Аксессуары", Image = "~/images/Toys.jpg"},
                new Category { Name = "Игрушки и игры", Image = "~/images/Toys.jpg"},
                new Category { Name = "Игры и головоломки", Image = "~/images/Toys.jpg", ParentId = 3},
                new Category { Name = "Кости и игры с использованием костей", Image = "~/images/Toys.jpg", ParentId = 4},
                new Category { Name = "Игрушки", Image = "~/images/Toys.jpg", ParentId = 3},
                new Category { Name = "Одежда", Image = "~/images/Toys.jpg"},
                new Category { Name = "Обувь", Image = "~/images/Toys.jpg"},
                new Category { Name = "Дом и стиль жизни", Image = "~/images/Toys.jpg"},
                new Category { Name = "Сумки и кошельки", Image = "~/images/Toys.jpg"}
            };

            List<Product> products = new List<Product>
            {
                new Product {Name = "Dnd Dice", Description="Набор негабаритных кубиков с цифрами, написанными чернилами по вашему выбору. Бросьте в кастрюлю под давлением, чтобы убедиться, что кости свободны от пузырьков.",
                             Price = 25.00M, Image = "~/images/Dnddice.jpg", Quantity=15, CategoryId=5, UserId="d797573b-73c0-4256-961b-9fd951ea1d4d" },
                new Product {Name = "Dnd Dice", Description="Набор негабаритных кубиков с цифрами, написанными чернилами по вашему выбору. Бросьте в кастрюлю под давлением, чтобы убедиться, что кости свободны от пузырьков.",
                             Price = 25.00M, Image = "~/images/Dnddice.jpg", Quantity=15, CategoryId=5, UserId="d797573b-73c0-4256-961b-9fd951ea1d4d" },
                new Product {Name = "Dnd Dice", Description="Набор негабаритных кубиков с цифрами, написанными чернилами по вашему выбору. Бросьте в кастрюлю под давлением, чтобы убедиться, что кости свободны от пузырьков.",
                             Price = 25.00M, Image = "~/images/Dnddice.jpg", Quantity=15, CategoryId=5, UserId="d797573b-73c0-4256-961b-9fd951ea1d4d" },
                new Product {Name = "Dnd Dice", Description="Набор негабаритных кубиков с цифрами, написанными чернилами по вашему выбору. Бросьте в кастрюлю под давлением, чтобы убедиться, что кости свободны от пузырьков.",
                             Price = 25.00M, Image = "~/images/Dnddice.jpg", Quantity=15, CategoryId=5, UserId="d797573b-73c0-4256-961b-9fd951ea1d4d" },
                new Product {Name = "Dnd Dice", Description="Набор негабаритных кубиков с цифрами, написанными чернилами по вашему выбору. Бросьте в кастрюлю под давлением, чтобы убедиться, что кости свободны от пузырьков.",
                             Price = 25.00M, Image = "~/images/Dnddice.jpg", Quantity=15, CategoryId=5, UserId="d797573b-73c0-4256-961b-9fd951ea1d4d" },
                new Product {Name = "Dnd Dice", Description="Набор негабаритных кубиков с цифрами, написанными чернилами по вашему выбору. Бросьте в кастрюлю под давлением, чтобы убедиться, что кости свободны от пузырьков.",
                             Price = 25.00M, Image = "~/images/Dnddice.jpg", Quantity=15, CategoryId=5, UserId="d797573b-73c0-4256-961b-9fd951ea1d4d" },
                new Product {Name = "Dnd Dice", Description="Набор негабаритных кубиков с цифрами, написанными чернилами по вашему выбору. Бросьте в кастрюлю под давлением, чтобы убедиться, что кости свободны от пузырьков.",
                             Price = 25.00M, Image = "~/images/Dnddice.jpg", Quantity=15, CategoryId=5, UserId="d797573b-73c0-4256-961b-9fd951ea1d4d" },
                new Product {Name = "Dnd Dice", Description="Набор негабаритных кубиков с цифрами, написанными чернилами по вашему выбору. Бросьте в кастрюлю под давлением, чтобы убедиться, что кости свободны от пузырьков.",
                             Price = 25.00M, Image = "~/images/Dnddice.jpg", Quantity=15, CategoryId=5, UserId="d797573b-73c0-4256-961b-9fd951ea1d4d" },
                new Product {Name = "Игрушечная лошадь", Description="Игрушечная лошадь для игрыю Очень качественные материалы и качественные краски.",
                             Price = 15.50M, Image = "~/images/Toys.jpg", Quantity=25, CategoryId=6, UserId="d797573b-73c0-4256-961b-9fd951ea1d4d" },
            };

            List<Review> reviews = new List<Review>
            {
                new Review { Rating = 4, Text="good dice", ReviewDate=DateTime.Now, ProductId=2, UserId="9668f43c-7c99-485b-849e-ba69a66e049f"},
                new Review { Rating = 5, Text="good dice", ReviewDate=DateTime.Now, ProductId=2, UserId="f6efc5ec-3a96-42af-a404-f076733a54ff"},
            };

            string sellerName = "VadimLinevich";
            string sellerEmail = "seller@gmail.com";
            string password = "123456";
            DateTime date = DateTime.Now;
            string userName = "VladLinevich";
            string userEmail = "user@gmail.com";
            string avatar = "~/images/avatar.png";

            if (await roleManager.FindByNameAsync("seller") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("seller"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(sellerName) == null)
            {
                ApplicationUser seller = new() { Email = sellerEmail, UserName = sellerName, Date_of_birth = date, Avatar = avatar };
                IdentityResult result = await userManager.CreateAsync(seller, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(seller, "seller");
                    await applicationDbContext.Cart.AddAsync(new Cart { UserId = seller.Id });
                }
            }
            if (await userManager.FindByNameAsync(userName) == null)
            {
                ApplicationUser user = new() { Email = userEmail, UserName = userName, Date_of_birth = date, Avatar = avatar };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                    await applicationDbContext.Cart.AddAsync(new Cart { UserId = user.Id });
                }
            }

            if (applicationDbContext.Category.IsNullOrEmpty())
            {
                foreach (var category in categories)
                {
                    await applicationDbContext.Category.AddAsync(category);                    
                }
            }

            if (applicationDbContext.Product.IsNullOrEmpty())
            {
                foreach (var product in products)
                {
                    await applicationDbContext.Product.AddAsync(product);
                }
            }

            if (applicationDbContext.Review.IsNullOrEmpty())
            {
                foreach (var review in reviews)
                {
                    await applicationDbContext.Review.AddAsync(review);
                }
            }

            applicationDbContext.SaveChanges();

        }
    }
}
