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
            ViewData["book"] = category;
            return View();
        }

        public IActionResult Trial()
        {
            return View((object) new List<int>() { 1,2,3});
        }

        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }
    }
}
