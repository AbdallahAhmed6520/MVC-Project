using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        //Register
        // BaseUrl/Account/Register
        public IActionResult Register()
        {
            return View();
        }
    }
}
