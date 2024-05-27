using KhumaloCraftWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KhumaloCraftWebApp.Controllers
{
    public class ProductController : Controller
    {
        
        ProductTable table = new ProductTable();

        [HttpPost]
        public ActionResult MyWork(ProductTable Product)
        {
            var result2 = table.InsertProduct(Product);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MyWork()
        {
            return View();
        }
    }

}
