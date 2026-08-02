using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverHeroComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        public _DiscoverHeroComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var now = DateTime.Now;

            var activeEventsQuery = _context.Events.Where(e => e.IsActive && e.EventDate >= now);

            var activeEventsCount = await activeEventsQuery.CountAsync();
            var usersCount = await _context.Users.CountAsync();

            var locations = await activeEventsQuery.Select(e => e.Location).ToListAsync();
            var citiesCount = locations
                .Select(l => l.Split(',')[0].Trim())
                .Distinct()
                .Count();

            var avgRating = await _context.Reviews.AnyAsync()
                ? Math.Round(await _context.Reviews.AverageAsync(r => r.Rating), 1)
                : 0;

            var weekAgo = now.AddDays(-7);
            var ticketsThisWeek = await _context.Tickets
                .CountAsync(t => t.PurchaseDate >= weekAgo && t.Status != "Refunded");

            var featuredEntity = await activeEventsQuery
                .Include(e => e.Category)
                .OrderByDescending(e => e.Capacity == 0 ? 0 : (double)(e.Capacity - e.RemainingCapacity) / e.Capacity)
                .FirstOrDefaultAsync();

            var lowStockEntity = await activeEventsQuery
                .Where(e => e.RemainingCapacity > 0)
                .OrderBy(e => e.RemainingCapacity)
                .FirstOrDefaultAsync();

            var vm = new HeroViewModel
            {
                ActiveEventsCount = activeEventsCount,
                UsersCount = usersCount,
                CitiesCount = citiesCount,
                AverageRating = (double)avgRating,
                TicketsThisWeek = ticketsThisWeek,
                Featured = featuredEntity == null ? null : new FeaturedEventVm
                {
                    EventId = featuredEntity.EventId,
                    Title = featuredEntity.Title,
                    Location = featuredEntity.Location,
                    EventDate = featuredEntity.EventDate,
                    Price = featuredEntity.Price,
                    RemainingCapacity = featuredEntity.RemainingCapacity,
                    CategoryName = featuredEntity.Category?.CategoryName ?? ""
                },
                LowStock = lowStockEntity == null ? null : new FeaturedEventVm
                {
                    EventId = lowStockEntity.EventId,
                    Title = lowStockEntity.Title,
                    Location = lowStockEntity.Location,
                    EventDate = lowStockEntity.EventDate,
                    Price = lowStockEntity.Price,
                    RemainingCapacity = lowStockEntity.RemainingCapacity,
                    CategoryName = lowStockEntity.Category?.CategoryName ?? ""
                }
            };

            return View(vm);
        }
    }
}