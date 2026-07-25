using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
