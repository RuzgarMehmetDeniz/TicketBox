using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Refunds.Commands;
using TicketBox.Application.Features.CQRS.Refunds.Queries;

namespace TicketBox.WebUI.Areas.Admin.Controllers
{
    public class AdminRefundController : Controller
    {
        private readonly IMediator _mediator;

        public AdminRefundController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> RefundList()
        {
            var values = await _mediator.Send(new GetAllRefundsQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult RefundCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefundCreate(CreateRefundCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(RefundList));
        }

        [HttpGet]
        public async Task<IActionResult> RefundUpdate(int id)
        {
            var value = await _mediator.Send(new GetRefundByIdQuery { RefundId = id });
            if (value == null)
                return NotFound();

            var command = new UpdateRefundCommand
            {
                RefundId = value.RefundId,
                Status = value.Status
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefundUpdate(UpdateRefundCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(RefundList));

            ModelState.AddModelError("", "Güncelleme yapılırken bir hata oluştu.");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefundDelete(int id)
        {
            await _mediator.Send(new DeleteRefundCommand { RefundId = id });
            return RedirectToAction(nameof(RefundList));
        }
    }
}