using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverUpComingEventComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        public _DiscoverUpComingEventComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var events = await _context.Events
                .Include(e => e.Category)
                .Include(e => e.CreatedByUser)
                .Include(e => e.Reviews)
                .Where(e => e.IsActive && e.EventDate >= DateTime.Now)
                .OrderBy(e => e.EventDate)
                .Take(5)
                .Select(e => new UpComingEventCardViewModel
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    CategoryName = e.Category.CategoryName,
                    Location = e.Location,
                    EventDate = e.EventDate,
                    Price = e.Price,
                    OrganizerName = e.CreatedByUser.Name + " " + e.CreatedByUser.Surname,
                    AttendeeCount = e.Capacity - e.RemainingCapacity,
                    FillPercentage = e.Capacity == 0 ? 0 : (int)Math.Round((e.Capacity - e.RemainingCapacity) * 100.0 / e.Capacity),
                    AverageRating = e.Reviews.Any() ? Math.Round(e.Reviews.Average(r => r.Rating), 1) : 0,
                    ImageUrl = e.ImageUrl
                })
                .ToListAsync();

            foreach (var ev in events)
                ev.IsLive = ev.FillPercentage >= 80;

            return View(events);
        }

      
    }
}