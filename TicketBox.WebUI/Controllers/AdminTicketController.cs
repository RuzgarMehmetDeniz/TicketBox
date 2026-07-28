using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Tickets.Commands;
using TicketBox.Application.Features.CQRS.Tickets.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminTicketController : Controller
    {
        private readonly IMediator _mediator;

        public AdminTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> TicketList()
        {
            var values = await _mediator.Send(new GetAllTicketsQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult TicketCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TicketCreate(CreateTicketCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var ticketId = await _mediator.Send(command);
            if (ticketId > 0)
                return RedirectToAction(nameof(TicketList));

            ModelState.AddModelError("", "Bilet oluşturulamadı. Etkinlik kapasitesi dolmuş veya geçersiz bir kupon kullanılmış olabilir.");
            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> TicketUpdate(int id)
        {
            var value = await _mediator.Send(new GetTicketByIdQuery { TicketId = id });
            if (value == null)
                return NotFound();

            var command = new UpdateTicketCommand
            {
                TicketId = value.TicketId,
                TicketImageUrl = value.TicketImageUrl,
                Status = value.Status,
                IsEmailSent = value.IsEmailSent
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TicketUpdate(UpdateTicketCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(TicketList));

            ModelState.AddModelError("", "Bilet güncellenirken bir hata oluştu.");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TicketDelete(int id)
        {
            await _mediator.Send(new DeleteTicketCommand { TicketId = id });
            return RedirectToAction(nameof(TicketList));
        }
    }
}