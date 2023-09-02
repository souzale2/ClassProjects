using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
