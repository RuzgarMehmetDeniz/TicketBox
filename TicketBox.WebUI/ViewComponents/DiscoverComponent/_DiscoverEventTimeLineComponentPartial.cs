using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverEventTimeLineComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        public _DiscoverEventTimeLineComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var events = await _context.Events
                .Where(e => e.IsActive && e.EventDate >= DateTime.Now)
                .OrderBy(e => e.EventDate)
                .Take(4)
                .Select(e => new EventTimelineItemViewModel
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Location = e.Location,
                    EventDate = e.EventDate
                })
                .ToListAsync();

            return View(events);
        }

       
    }
}