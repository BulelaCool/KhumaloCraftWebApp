using KhumaloCraftWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KhumaloCraftWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginModel login;

        public LoginController()
        {
            login = new LoginModel();
        }

        [HttpPost]
        public ActionResult Login(string Name, string Surname, string Email, string Password)
        {
            var loginModel = new LoginModel();
            int UserID = loginModel.SelectUser(Name, Surname, Email, Password);
            if (UserID != -1)
            {
                // Store userID in session
                HttpContext.Session.SetInt32("UserID", UserID);

                // User found, proceed with login logic
                return RedirectToAction("Index", "Home", new { UserID = UserID });
            }
            else
            {
                // User not found, return login view with an error message
                ViewBag.ErrorMessage = "Login failed. Please try again.";
                return View("~/Views/Home/Login.cshtml");
            }
        }
    }
}
