using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFLTeams.Models;

namespace NFLTeams.Controllers
{
    public class HomeController : Controller
    {
        private TeamContext context;
        public HomeController(TeamContext ctx) => context = ctx;

        public ViewResult Index(TeamsViewModel model)
        {
            // store active conference and division in session
            var session = new NFLSession(HttpContext.Session);
            session.SetActiveConf(model.ActiveConf);
            session.SetActiveDiv(model.ActiveDiv);

            // get conferences and divisions from database
            model.Conferences = context.Conferences.ToList();
            model.Divisions = context.Divisions.ToList();

            // get teams from database - filter by conference and division
            IQueryable<Team> query = context.Teams.OrderBy(t => t.Name);
            if (model.ActiveConf != "all")
                query = query.Where(
                    t => t.Conference.ConferenceID.ToLower() == model.ActiveConf.ToLower());
            if (model.ActiveDiv != "all")
                query = query.Where(
                    t => t.Division.DivisionID.ToLower() == model.ActiveDiv.ToLower());
            model.Teams = query.ToList();

            // pass view model to view
            return View(model);
        }

        public ViewResult Details(string id)
        {
            var isItAlive = ViewBag.InViewBag;
            var isItAlive1 = ViewData["InViewData"];
            var isItAlive2 = TempData["InTempData"];
            var isItAlive3 = TempData["InTempData"];

            // get current conference and division from session
            // and pass them to the view in the view model
            var session = new NFLSession(HttpContext.Session);
            var model = new TeamsViewModel
            {
                Team = context.Teams
                    .Include(t => t.Conference)
                    .Include(t => t.Division)
                    .FirstOrDefault(t => t.TeamID == id) ?? new Team(),
                ActiveDiv = session.GetActiveDiv(),
                ActiveConf = session.GetActiveConf()
            };
            return View(model);
        }

        public ViewResult Test()
        {
            ViewBag.InViewBag = "InViewBag";
            ViewData["InViewData"] = "InViewData";
            TempData["InTempData"] = "InTempData";

            ViewBag.Teams = context.Teams.OrderBy(o => o.Name).ToList();
            return View();
        }

        [Route("ToDiffSite/{website}")]
        public RedirectResult Test(string website)
        {
            return Redirect("https://" + website);
        

        }
        
        [Route("{action}/{method}/{id?}")]
        public RedirectToActionResult Site(int method, string id = "ari")
        {

            ViewBag.InViewBag = "InViewBag";
            ViewData["InViewData"] = "InViewData";
            TempData["InTempData"] = "InTempData";

            var smethodAndController = (method == 1) ? ("Index", "Favorites") : ("Details", "Home");
            return RedirectToAction(smethodAndController.Item1, smethodAndController.Item2, new {id = id});
        }

    }
}