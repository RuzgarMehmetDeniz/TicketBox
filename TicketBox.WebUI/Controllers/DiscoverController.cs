using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Controllers
{
    public class DiscoverController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
