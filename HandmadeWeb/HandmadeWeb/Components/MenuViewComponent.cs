using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using HandmadeWeb.Models;

namespace HandmadeWeb.Components
{
    public class MenuViewComponent: ViewComponent
    {
        private List<MenuItem> _menuItems = new List<MenuItem>
        {
            new MenuItem{ Controller="Home", Action="Index", Text="Главная"},
            new MenuItem{ Controller="Category", Action="Index",
            Text="Каталог"},
            new MenuItem{ IsPage=true, Area="Identity", Page="/Account/Login",
            Text="Войти"}
        };

        public IViewComponentResult Invoke()
        {
            foreach (var menuItem in _menuItems)
            {
                if (ViewContext.RouteData.Values["controller"] != null)
                {
                    if (ViewContext.RouteData.Values["controller"].Equals(menuItem.Controller))
                    {
                        menuItem.Active = "active";
                    }
                }
                else
                {
                    if (ViewContext.RouteData.Values["area"].Equals(menuItem.Area))
                    {
                        menuItem.Active = "active";
                    }
                }
            }
            return View(_menuItems);
        }
    }
}
