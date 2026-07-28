using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatMessages.Commands;
using TicketBox.Application.Features.CQRS.ChatMessages.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminChatMessageController : Controller
    {
        private readonly IMediator _mediator;

        public AdminChatMessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ChatMessageList()
        {
            var values = await _mediator.Send(new GetAllChatMessagesQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult ChatMessageCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChatMessageCreate(CreateChatMessageCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(ChatMessageList));

            ModelState.AddModelError("", "Mesaj eklenirken bir hata oluştu.");
            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> ChatMessageUpdate(int id)
        {
            var value = await _mediator.Send(new GetChatMessageByIdQuery { ChatMessageId = id });
            if (value == null)
                return NotFound();

            var command = new UpdateChatMessageCommand
            {
                ChatMessageId = value.ChatMessageId,
                Content = value.Content
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChatMessageUpdate(UpdateChatMessageCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(ChatMessageList));

            ModelState.AddModelError("", "Mesaj güncellenirken bir hata oluştu.");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChatMessageDelete(int id)
        {
            await _mediator.Send(new DeleteChatMessageCommand { ChatMessageId = id });
            return RedirectToAction(nameof(ChatMessageList));
        }
    }
}