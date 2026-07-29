using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TicketBox.Persistance.Context;
using TicketBox.WebUI.Models; 

namespace TicketBox.WebUI.ViewComponents.DiscoverComponent
{
    public class _DiscoverCategoriesComponentPartial : ViewComponent
    {
        private readonly TicketContext _context;

        public _DiscoverCategoriesComponentPartial(TicketContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.CategoryId)
                .Take(6)
                .Select(c => new CategoryCardViewModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    IconUrl = c.IconUrl,
                    EventCount = c.Events.Count(e => e.IsActive)
                })
                .ToListAsync();

            return View(categories);
        }
    }
}