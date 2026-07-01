using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Application.Features.Mediator.Events.Handlers;
using TicketBox.Application.Features.Mediator.Events.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> EventList()
        {
            var values = await _mediator.Send(new GetEventQuery());
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("EventList");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _mediator.Send(id);
            return RedirectToAction("EventList");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(int id, UpdateEventCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("EventList");
        }
    }
}