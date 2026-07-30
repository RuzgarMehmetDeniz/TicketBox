using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverEventsComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        public _DiscoverEventsComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? category = null)
        {
            var query = _context.Events
                .Include(e => e.Category)
                .Include(e => e.CreatedByUser)
                .Include(e => e.Reviews)
                .Where(e => e.IsActive && e.EventDate >= DateTime.Now);

            // category parametresine artık burada gerek yok, JS tarafında filtrelenecek
            var events = await query
                .OrderBy(e => e.EventDate)
                .Take(20) // client-side filtre için yeterli havuz
                .Select(e => new EventCardViewModel
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