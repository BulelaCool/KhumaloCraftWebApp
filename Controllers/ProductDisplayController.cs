using KhumaloCraftWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KhumaloCraftWebApp.Controllers
{
    public class ProductDisplayController : Controller
    {
        public IActionResult Index()
        {
            List<ProductTable> products = ProductTable.GetAllProducts();
            return View(products);
        }
    }
}