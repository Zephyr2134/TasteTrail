using Microsoft.AspNetCore.Mvc;
using EPiServer.Web.Mvc;
using TasteTrail.Models.Pages;

namespace TasteTrail.Controllers
{
    public class StartPageController : PageController<StartPage>
    {
        public IActionResult Index(StartPage currentPage)
        {
            return View(currentPage);
        }
    }
}
