using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/NotFound404")]
        public IActionResult NotFound404()
        {
            return View("NotFound404");
        }

        [Route("Error/Unauthorized401")]
        public IActionResult Unauthorized401()
        {
            return View("Unauthorized401");
        }

        [Route("Error/ServerError500")]
        public IActionResult ServerError500()
        {
            return View("ServerError500");
        }

        // 404/401/500 dışındaki diğer status kodları (403, 400 vb.) için genel fallback
        [Route("Error/{statusCode:int}")]
        public IActionResult OtherStatusCode(int statusCode)
        {
            return View("ServerError");
        }

        // UseExceptionHandler("/Error") burayı çağırır (500 exception durumunda)
        [Route("Error")]
        public IActionResult Error()
        {
            return View("ServerError");
        }
    }
}