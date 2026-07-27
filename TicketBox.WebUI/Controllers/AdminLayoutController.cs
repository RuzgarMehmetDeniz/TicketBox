using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // burda benım ıstedıgım tema var ama usttekı layout olarak kullanılcak 
        public IActionResult Index2()
        {
            return View();
        }
    }
}
