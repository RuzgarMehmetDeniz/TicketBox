using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverPopularEventComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        public _DiscoverPopularEventComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var events = await _context.Events
                .Include(e => e.Category)
                .Where(e => e.IsActive && e.EventDate >= DateTime.Now)
                .OrderByDescending(e => e.Capacity - e.RemainingCapacity) // en çok katılımcı = en popüler
                .Take(6)
                .Select(e => new PopularEventItemViewModel
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    CategoryName = e.Category.CategoryName,
                    ImageUrl = e.ImageUrl
                })
                .ToListAsync();

            return View(events);
        }

      
    }
}