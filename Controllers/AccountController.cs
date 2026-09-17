using Microsoft.AspNetCore.Mvc;

namespace CrimeReportingSystem.Controllers
{
    public class AccountController : Controller
    {
        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                return RedirectToAction("Admin", "CrimeReports");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
