using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Controllers
{
    public class DiscoverController : Controller
    {
        // Açık tema 1
        public IActionResult Index()
        {
            return View();
        }
        // Kapalı Tema 1
        public IActionResult Index2()
        {
            return View();
        }
        // Kapalı Tema 1
        [Authorize]
        public IActionResult Index3()
        {
            return View();
        }
    }
}
