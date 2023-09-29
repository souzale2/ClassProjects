using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GuitarShop.Models;
namespace GuitarShop.Controllers
{

    

    public class HomeController : Controller
    {
        //Later
        ShopContext context; 
        public HomeController(ShopContext context)
        {
            this.context = context;
        }
        public IActionResult Index(string category = "Guitars")
        {
            ViewBag.Categories = new List<string>() { "Guitars", "Basses", "Drums" };
            ViewBag.SelectedCategoryName = category;

            return View();
        }

        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }
    }
}
