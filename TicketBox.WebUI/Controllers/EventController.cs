using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Events.Queries;
using TicketBox.Application.Features.CQRS.Favorites.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly IMediator _mediator;
        public EventController(IMediator mediator) => _mediator = mediator;

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _mediator.Send(new GetEventDetailQuery { EventId = id });
            if (result == null) return NotFound();

            bool isFavorited = false;
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var favId = await _mediator.Send(new IsFavoritedQuery { AppUserId = userId, EventId = id });
                isFavorited = favId.HasValue;
            }
            ViewData["IsFavorited"] = isFavorited;

            return View(result);
        }
    }
}