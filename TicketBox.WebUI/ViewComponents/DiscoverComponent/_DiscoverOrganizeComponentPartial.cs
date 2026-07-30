using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverOrganizeComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        public _DiscoverOrganizeComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var organizers = await _context.Users
                .Where(u => u.CreatedEvents.Any(e => e.IsActive))
                .OrderByDescending(u => u.CreatedEvents.Count(e => e.IsActive))
                .Take(5)
                .Select(u => new OrganizerCardViewModel
                {
                    AppUserId = u.Id,
                    FullName = u.Name + " " + u.Surname,
                    EventCount = u.CreatedEvents.Count(e => e.IsActive),
                    ProfileImageUrl = u.ProfileImageUrl
                })
                .ToListAsync();

            return View(organizers);
        }

       
    }
}