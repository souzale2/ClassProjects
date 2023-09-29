using FutureValue.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FutureValue.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            //Demonstation

            var myString = new StringBuilder("Glorious Embrance");
            while (myString.Length > 3)
            {
                myString.Remove(myString.Length - 1, 1);
            }

            ViewBag.FV = 0;
            return View();
        }

        [HttpPost]
        public IActionResult Index(FutureValueModel model)
        {
           
            if (ModelState.IsValid)
            {
                ViewBag.FV = model.CalculateFutureValue();
                
            }
            else
            {
                ViewBag.FV = 0;
            }
            return View(model);
        }
    }
}