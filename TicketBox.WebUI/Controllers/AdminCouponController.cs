using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Coupons.Commands;
using TicketBox.Application.Features.CQRS.Coupons.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminCouponController : Controller
    {
        private readonly IMediator _mediator;

        public AdminCouponController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> CouponList()
        {
            var values = await _mediator.Send(new GetCouponQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CouponCreate(CreateCouponCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction("CouponList");
        }

        [HttpGet]
        public async Task<IActionResult> CouponUpdate(int id)
        {
            var value = await _mediator.Send(new GetCouponByIdQuery(id));
            if (value == null)
            {
                return RedirectToAction("CouponList");
            }

            // GetCouponByIdQueryResult modelini UpdateCouponCommand nesnesine dönüştürüyoruz
            var command = new UpdateCouponCommand
            {
                CouponId = value.CouponId,
                Code = value.Code,
                DiscountPercentage = value.DiscountPercentage,
                ExpiryDate = value.ExpiryDate,
                IsActive = value.IsActive,
                UsageLimit = value.UsageLimit,
                UsedCount = value.UsedCount
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CouponUpdate(UpdateCouponCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction("CouponList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CouponDelete(int id)
        {
            await _mediator.Send(new DeleteCouponCommand(id));
            return RedirectToAction("CouponList");
        }
    }
}