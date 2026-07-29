using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverNewsLetterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
