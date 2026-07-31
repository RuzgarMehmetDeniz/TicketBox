using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Reviews.Commands;

namespace TicketBox.WebUI.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IMediator _mediator;
        public ReviewController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int eventId, int rating, string? comment)
        {
            if (rating < 1 || rating > 5)
            {
                TempData["ReviewError"] = "Lütfen 1 ile 5 arasında bir puan seçin.";
                return RedirectToAction("Detail", "Event", new { id = eventId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _mediator.Send(new CreateReviewCommand
            {
                EventId = eventId,
                AppUserId = userId,
                Rating = rating,
                Comment = comment
            });

            return RedirectToAction("Detail", "Event", new { id = eventId });
        }
    }
}