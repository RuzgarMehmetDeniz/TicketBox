using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverSatsComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        public _DiscoverSatsComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var totalEvents = await _context.Events.CountAsync(e => e.IsActive);
            var soldTickets = await _context.Tickets.CountAsync();

            var organizerCount = await _context.Events
                .Where(e => e.IsActive)
                .Select(e => e.CreatedByUserId)
                .Distinct()
                .CountAsync();

            // Şehir sayısı: Cities component'indeki bilinen şehir listesiyle aynı mantık
            var knownCities = new[] { "İstanbul", "Ankara", "İzmir", "Antalya", "Bursa", "Adana", "Konya" };
            var locations = await _context.Events
                .Where(e => e.IsActive)
                .Select(e => e.Location)
                .ToListAsync();

            var cityCount = knownCities
                .Count(city => locations.Any(loc => loc != null && loc.Contains(city, StringComparison.OrdinalIgnoreCase)));

            var model = new PlatformStatsViewModel
            {
                TotalEvents = totalEvents,
                SoldTickets = soldTickets,
                CityCount = cityCount,
                OrganizerCount = organizerCount
            };

            return View(model);
        }

      
    }
}