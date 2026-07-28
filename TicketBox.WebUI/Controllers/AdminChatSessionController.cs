using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatSessions.Commands;
using TicketBox.Application.Features.CQRS.ChatSessions.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminChatSessionController : Controller
    {
        private readonly IMediator _mediator;

        public AdminChatSessionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ChatSessionList()
        {
            var values = await _mediator.Send(new GetAllChatSessionsQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult ChatSessionCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChatSessionCreate(CreateChatSessionCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var sessionId = await _mediator.Send(command);
            if (sessionId > 0)
                return RedirectToAction(nameof(ChatSessionList));

            ModelState.AddModelError("", "Sohbet oturumu oluşturulurken bir hata oluştu.");
            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> ChatSessionUpdate(int id)
        {
            var value = await _mediator.Send(new GetChatSessionByIdQuery { ChatSessionId = id });
            if (value == null)
                return NotFound();

            var command = new UpdateChatSessionCommand
            {
                ChatSessionId = value.ChatSessionId,
                AppUserId = value.AppUserId
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChatSessionUpdate(UpdateChatSessionCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(ChatSessionList));

            ModelState.AddModelError("", "Sohbet oturumu güncellenirken bir hata oluştu.");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChatSessionDelete(int id)
        {
            await _mediator.Send(new DeleteChatSessionCommand { ChatSessionId = id });
            return RedirectToAction(nameof(ChatSessionList));
        }
    }
}