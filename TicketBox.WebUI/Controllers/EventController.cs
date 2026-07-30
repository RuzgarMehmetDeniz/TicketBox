using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Events.Queries;

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

            return View(result); // Result direkt view model olarak kullanılıyor
        }
    }
}