using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Favorites.Queries;
using TicketBox.Application.Features.CQRS.Payments.Queries;
using TicketBox.Application.Features.CQRS.Refunds.Commands;
using TicketBox.Application.Features.CQRS.Refunds.Queries;
using TicketBox.Application.Features.CQRS.Reviews.Queries;
using TicketBox.Application.Features.CQRS.Tickets.Commands;
using TicketBox.Application.Features.CQRS.Tickets.Queries;
using TicketBox.Domain.Entities;
using TicketBox.WebUI.Models;


namespace TicketBox.WebUI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestRefund(int ticketId, string reason)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ticket = await _mediator.Send(new GetTicketByIdQuery { TicketId = ticketId });
            if (ticket == null || ticket.AppUserId != userId)
                return Json(new { success = false, message = "Bilet bulunamadı." });

            if (ticket.Status != "Active")
                return Json(new { success = false, message = "Bu bilet için iade talebi oluşturulamaz." });

            var payment = await _mediator.Send(new GetPaymentByTicketIdQuery { TicketId = ticketId });
            if (payment == null)
                return Json(new { success = false, message = "Bu bilete ait ödeme kaydı bulunamadı." });

            var existingRefund = await _mediator.Send(new GetRefundByTicketIdQuery { TicketId = ticketId });
            if (existingRefund != null)
                return Json(new { success = false, message = "Bu bilet için zaten bir iade talebi mevcut." });


            await _mediator.Send(new CreateRefundCommand
            {
                TicketId = ticketId,
                PaymentId = payment.PaymentId,
                RefundAmount = ticket.Price,
                Reason = string.IsNullOrWhiteSpace(reason) ? "Belirtilmedi" : reason
            });

            await _mediator.Send(new UpdateTicketCommand
            {
                TicketId = ticketId,
                TicketImageUrl = ticket.TicketImageUrl,
                Status = "İade Talep Edildi",
                IsEmailSent = ticket.IsEmailSent
            });

            return Json(new { success = true });
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var vm = new ProfileViewModel
            {
                AppUserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Age = user.Age,
                City = user.City,
                Country = user.Country,
                ProfileImageUrl = user.ProfileImageUrl,
                CreatedDate = user.CreatedDate,
                Tickets = await _mediator.Send(new GetMyTicketsQuery { AppUserId = user.Id }),
                Favorites = await _mediator.Send(new GetMyFavoritesQuery { AppUserId = user.Id }),
                Reviews = await _mediator.Send(new GetMyReviewsQuery { AppUserId = user.Id })
            };

            return View(vm);
        }
    }
}