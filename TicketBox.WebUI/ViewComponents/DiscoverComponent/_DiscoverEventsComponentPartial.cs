using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverEventsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
            {
            return View(); 
        }
    }
}
