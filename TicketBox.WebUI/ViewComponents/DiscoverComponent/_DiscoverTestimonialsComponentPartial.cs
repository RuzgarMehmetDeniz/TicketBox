using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverTestimonialsComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        public _DiscoverTestimonialsComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reviews = await _context.Reviews
                .Include(r => r.AppUser)
                .Where(r => r.Rating >= 4 && !string.IsNullOrEmpty(r.Comment))
                .OrderByDescending(r => r.CreatedDate)
                .Take(3)
                .Select(r => new TestimonialItemViewModel
                {
                    ReviewId = r.ReviewId,
                    FullName = r.AppUser.Name + " " + r.AppUser.Surname,
                    City = r.AppUser.City,
                    Rating = r.Rating,
                    Comment = r.Comment
                })
                .ToListAsync();

            return View(reviews);
        }

      
    }
}