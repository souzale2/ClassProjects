using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;


namespace GuitarShop.Controllers
{
    public class ExampleController : Controller
    {

        private ContextCoordinates context { get; set; }
        public ExampleController(ContextCoordinates ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ModelsDouble modelsDouble = new();

            modelsDouble.CoordinatesList = context.CellCoordinates.ToList<CellCoordinates>();
            modelsDouble.CoordinatesIndividual = new CellCoordinates();
            
            for(int i = 0; i < 50; i++)
            {
                Console.WriteLine(i);
            }
            return View(modelsDouble);
        }
        [HttpPost]
        public IActionResult Index(ModelsDouble models)
        {
            if (!ModelState.IsValid)
            {
                context.Update(models.CoordinatesIndividual);
                context.SaveChanges();

            }
            
            return RedirectToAction("Index", "Example");
        }
    }
}
