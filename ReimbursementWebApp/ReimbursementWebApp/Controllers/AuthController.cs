using Microsoft.AspNetCore.Mvc;

namespace ReimbursementWebApp.Controllers
{
    public class AuthController: Controller
    {
        public IActionResult LoginPage()
        {
            return View();
        }
    }
}
