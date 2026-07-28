using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Notifications.Commands;
using TicketBox.Application.Features.CQRS.Notifications.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminNotificationController : Controller
    {
        private readonly IMediator _mediator;
        public AdminNotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ============ LİSTELEME ============
        [HttpGet]
        public async Task<IActionResult> NotificationList()
        {
            var notifications = await _mediator.Send(new GetAllNotificationsQuery());
            return View(notifications);
        }

        // ============ EKLEME - GET ============
        [HttpGet]
        public IActionResult NotificationCreate()
        {
            return View();
        }

        // ============ EKLEME - POST ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NotificationCreate(CreateNotificationCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(NotificationList));
        }

        // ============ GÜNCELLEME - GET ============
        [HttpGet]
        public async Task<IActionResult> NotificationUpdate(int id)
        {
            var notification = await _mediator.Send(new GetNotificationByIdQuery { NotificationId = id });
            if (notification == null)
                return NotFound();

            var command = new UpdateNotificationCommand
            {
                NotificationId = notification.NotificationId,
                Title = notification.Title,
                Message = notification.Message,
                IsRead = notification.IsRead
            };
            return View(command);
        }

        // ============ GÜNCELLEME - POST ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NotificationUpdate(UpdateNotificationCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(NotificationList));
        }

        // ============ SİLME ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NotificationDelete(int id)
        {
            await _mediator.Send(new DeleteNotificationCommand { NotificationId = id });
            return RedirectToAction(nameof(NotificationList));
        }
    }
}