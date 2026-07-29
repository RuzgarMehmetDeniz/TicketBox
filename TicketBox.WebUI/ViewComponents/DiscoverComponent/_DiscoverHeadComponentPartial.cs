using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
