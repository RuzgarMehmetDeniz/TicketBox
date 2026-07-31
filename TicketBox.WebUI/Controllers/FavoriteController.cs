using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Favorites.Commands;
using TicketBox.Application.Features.CQRS.Favorites.Queries;

namespace TicketBox.WebUI.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IMediator _mediator;
        public FavoriteController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingId = await _mediator.Send(new IsFavoritedQuery { AppUserId = userId, EventId = eventId });

            if (existingId.HasValue)
            {
                await _mediator.Send(new DeleteFavoriteCommand { FavoriteId = existingId.Value });
                return Json(new { favorited = false });
            }

            var newId = await _mediator.Send(new CreateFavoriteCommand { AppUserId = userId, EventId = eventId });
            return Json(new { favorited = newId != 0 });
        }
    }
}