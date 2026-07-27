using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents.AdminLayoutComponent
{
    public class _AdminLayoutHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
