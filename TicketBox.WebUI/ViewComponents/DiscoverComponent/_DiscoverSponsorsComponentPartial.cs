using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverSponsorsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
