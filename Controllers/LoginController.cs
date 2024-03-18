using Microsoft.AspNetCore.Mvc;
using QRMenu_TabGida.Data;
using QRMenu_TabGida.Models;

namespace QRMenu_TabGida.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Control(string username, string password)
        {
            var user = _context.Set<ApplicationUser>().Where(x => x.UserName == username && x.Password == password);
            if (user.Count() == 0)
            {
                var brandUser = _context.Set<BrandUser>().Where(x => x.UserName == username && x.Password == password);
                if (brandUser.Count() == 0)
                {
                    var restaurantUser = _context.Set<RestaurantUser>().Where(x => x.UserName == username && x.Password == password);
                    if (restaurantUser.Count() == 0)
                    {
                        return Redirect("/Login/Index");
                    }
                    else
                    {
                        Response.Cookies.Append("username", $"{restaurantUser.First().UserName}");
                        Response.Cookies.Append("role", "RestaurantUser");
                        return Redirect("/Home/Index");
                    }
                }
                else
                {
                    Response.Cookies.Append("username", $"{brandUser.First().UserName}");
                    Response.Cookies.Append("role", "BrandUser");
                    return Redirect("/Home/Index");
                }
            }
            else
            {
                Response.Cookies.Append("username", $"{user.First().UserName}");
                Response.Cookies.Append("role", "User");
                return Redirect("/Home/Index");
            }
        }
    }
}
