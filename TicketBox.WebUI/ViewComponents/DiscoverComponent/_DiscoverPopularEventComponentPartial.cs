using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverPopularEventComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
