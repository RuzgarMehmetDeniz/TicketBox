using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverNavbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
