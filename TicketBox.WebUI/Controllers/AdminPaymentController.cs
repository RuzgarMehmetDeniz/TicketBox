using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Payments.Commands;
using TicketBox.Application.Features.CQRS.Payments.Queries;

namespace TicketBox.WebUI.Areas.Admin.Controllers
{
    public class AdminPaymentController : Controller
    {
        private readonly IMediator _mediator;

        public AdminPaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> PaymentList()
        {
            var values = await _mediator.Send(new GetAllPaymentsQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult PaymentCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentCreate(CreatePaymentCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(PaymentList));
        }

        [HttpGet]
        public async Task<IActionResult> PaymentUpdate(int id)
        {
            var value = await _mediator.Send(new GetPaymentByIdQuery { PaymentId = id });
            if (value == null)
                return NotFound();

            var command = new UpdatePaymentCommand
            {
                PaymentId = value.PaymentId,
                Status = value.Status,
                TransactionReference = value.TransactionReference
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentUpdate(UpdatePaymentCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(PaymentList));

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentDelete(int id)
        {
            await _mediator.Send(new DeletePaymentCommand { PaymentId = id });
            return RedirectToAction(nameof(PaymentList));
        }
    }
}