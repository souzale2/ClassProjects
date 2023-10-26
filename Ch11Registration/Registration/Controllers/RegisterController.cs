using Microsoft.AspNetCore.Mvc;
using Registration.Models;

namespace Registration.Controllers
{
    public class RegisterController : Controller
    {
        private RegistrationContext context;
        public RegisterController(RegistrationContext ctx) => context = ctx;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(Customer customer)
        {
            //Further Checking
            //Check if characters of Useraname contains "your" name and
            //characters after it (only)

            string usernameKey = nameof(customer.Username); //"Username"

            //if(ModelState.GetValidationState(usernameKey) == 
            //        Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
            //{
            //    //Further check
            //    if (customer.Username.EndsWith("Patrick"))
            //    {
            //        ModelState.AddModelError(usernameKey,
            //            "Name end with 'Patrick'. It should end with something other than 'Patrick'");
            //    }
            //}


            if (TempData["okEmail"] == null)
            {
                string msg = Check.EmailExists(context, customer.EmailAddress);
                if (!String.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(Customer.EmailAddress), msg);
                }
            }

            if (ModelState.IsValid)
            {
                context.Customers.Add(customer);
                context.SaveChanges();
                return RedirectToAction("Welcome");
            }
            else
            {
                return View(customer);
            }
        }

        public IActionResult Welcome() => View();
    }
}
