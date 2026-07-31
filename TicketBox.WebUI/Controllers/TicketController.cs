using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Coupons.Queries;
using TicketBox.Application.Features.CQRS.Events.Queries;
using TicketBox.Application.Features.CQRS.Payments.Commands;
using TicketBox.Application.Features.CQRS.Tickets.Commands;
using TicketBox.Application.Features.CQRS.Tickets.Queries;
using TicketBox.Application.Features.CQRS.Tickets.Results;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.Controllers
{
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;
        public TicketController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        public async Task<IActionResult> Reservation(int eventId)
        {
            var ev = await _mediator.Send(new GetEventDetailQuery { EventId = eventId });
            if (ev == null) return NotFound();

            if (ev.RemainingCapacity <= 0)
            {
                TempData["Error"] = "Bu etkinlik için kontenjan kalmadı.";
                return RedirectToAction("Detail", "Event", new { id = eventId });
            }

            var allCoupons = await _mediator.Send(new GetCouponQuery());
            var validCoupons = allCoupons
                .Where(c => c.IsActive
                         && c.ExpiryDate >= DateTime.Now
                         && (c.UsageLimit == null || c.UsedCount < c.UsageLimit))
                .Select(c => new CouponOptionViewModel { Code = c.Code, DiscountPercentage = c.DiscountPercentage })
                .ToList();

            var vm = new ReservationViewModel
            {
                EventId = ev.EventId,
                Title = ev.Title,
                ImageUrl = ev.ImageUrl,
                EventDate = ev.EventDate,
                Location = ev.Location,
                Price = ev.Price,
                RemainingCapacity = ev.RemainingCapacity,
                AvailableCoupons = validCoupons
            };

            return View(vm);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservation(ReservationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kupon kontrolü (varsa) — geçersizse hemen kullanıcıya geri bildir
            int? couponId = null;
            if (!string.IsNullOrWhiteSpace(model.CouponCode))
            {
                var coupons = await _mediator.Send(new GetCouponQuery());
                var coupon = coupons.FirstOrDefault(c =>
                    c.Code == model.CouponCode && c.IsActive && c.ExpiryDate >= DateTime.Now);

                if (coupon == null)
                {
                    ModelState.AddModelError("", "Kupon geçersiz veya süresi dolmuş.");
                    return View(model);
                }

                if (coupon.UsageLimit.HasValue && coupon.UsedCount >= coupon.UsageLimit.Value)
                {
                    ModelState.AddModelError("", "Kupon kullanım limiti doldu.");
                    return View(model);
                }

                couponId = coupon.CouponId;
            }

            var ticketIds = new List<int>();

            for (int i = 0; i < model.Quantity; i++)
            {
                var ticketId = await _mediator.Send(new CreateTicketCommand
                {
                    EventId = model.EventId,
                    AppUserId = userId,
                    CouponId = couponId
                });

                if (ticketId == 0)
                {
                    ModelState.AddModelError("", i == 0
                        ? "Rezervasyon oluşturulamadı — kontenjan kalmamış veya kupon geçersiz olabilir."
                        : $"{i}. bilet oluşturulduktan sonra kontenjan doldu, işlem burada durduruldu.");
                    // Bu ana kadar oluşan biletler geçerlidir; kullanıcıyı onlarla onaya götürelim
                    if (ticketIds.Any())
                    {
                        TempData["TicketIds"] = string.Join(",", ticketIds);
                        return RedirectToAction("Confirmation");
                    }
                    return View(model);
                }

                // Bilet fiyatını almak için oluşturulan bileti çekiyoruz
                var ticket = await _mediator.Send(new GetTicketByIdQuery { TicketId = ticketId });

                // Ödeme kaydı — test/simülasyon modu, gerçek gateway yok
                var transactionRef = Guid.NewGuid().ToString("N");
                var paymentId = await _mediator.Send(new CreatePaymentCommand
                {
                    TicketId = ticketId,
                    Amount = ticket.Price,
                    PaymentMethod = model.PaymentMethod,
                    TransactionReference = transactionRef
                });

                // Simülasyonda ödeme her zaman başarılı sayılıyor
                await _mediator.Send(new UpdatePaymentCommand
                {
                    PaymentId = paymentId,
                    Status = "Başarılı",
                    TransactionReference = transactionRef
                });

                ticketIds.Add(ticketId);
            }

            TempData["TicketIds"] = string.Join(",", ticketIds);
            return RedirectToAction("Confirmation");
        }

        public async Task<IActionResult> Confirmation()
        {
            var idsCsv = TempData["TicketIds"] as string;
            if (string.IsNullOrEmpty(idsCsv))
                return RedirectToAction("Index3", "Discover");

            var ids = idsCsv.Split(',').Select(int.Parse).ToList();
            var tickets = new List<GetTicketByIdQueryResult>();

            foreach (var id in ids)
            {
                var ticket = await _mediator.Send(new GetTicketByIdQuery { TicketId = id });
                if (ticket != null) tickets.Add(ticket);
            }

            return View(tickets);
        }
    }
}