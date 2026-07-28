using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AuditLogs.Commands;
using TicketBox.Application.Features.CQRS.AuditLogs.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminAuditLogController : Controller
    {
        private readonly IMediator _mediator;
        public AdminAuditLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ============ LİSTELEME ============
        [HttpGet]
        public async Task<IActionResult> AuditLogList()
        {
            var logs = await _mediator.Send(new GetAllAuditLogsQuery());
            return View(logs);
        }

        // ============ EKLEME - GET ============
        [HttpGet]
        public IActionResult AuditLogCreate()
        {
            return View();
        }

        // ============ EKLEME - POST ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuditLogCreate(CreateAuditLogCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(AuditLogList));
        }

        // ============ GÜNCELLEME - GET ============
        [HttpGet]
        public async Task<IActionResult> AuditLogUpdate(int id)
        {
            var log = await _mediator.Send(new GetAuditLogByIdQuery { AuditLogId = id });
            if (log == null)
                return NotFound();

            var command = new UpdateAuditLogCommand
            {
                AuditLogId = log.AuditLogId,
                Action = log.Action,
                Details = log.Details
            };
            return View(command);
        }

        // ============ GÜNCELLEME - POST ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuditLogUpdate(UpdateAuditLogCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(AuditLogList));
        }

        // ============ SİLME ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuditLogDelete(int id)
        {
            await _mediator.Send(new DeleteAuditLogCommand { AuditLogId = id });
            return RedirectToAction(nameof(AuditLogList));
        }
    }
}