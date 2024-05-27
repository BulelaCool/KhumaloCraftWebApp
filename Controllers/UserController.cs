using KhumaloCraftWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KhumaloCraftWebApp.Controllers
{
    public class UserController : Controller
    {
        public UserTable UserTbl = new UserTable();



        [HttpPost]
        public ActionResult AboutUs(UserTable Users)
        {
            var result = UserTbl.InsertUser(Users);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult About()
        {
            return View(UserTbl);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
