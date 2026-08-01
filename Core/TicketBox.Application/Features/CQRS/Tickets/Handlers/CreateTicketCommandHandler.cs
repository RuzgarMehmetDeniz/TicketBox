using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Tickets.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Services;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.Tickets.Handlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId);
            if (eventEntity == null || eventEntity.RemainingCapacity <= 0)
                return 0;

            decimal finalPrice = eventEntity.Price;
            Coupon coupon = null;

            if (request.CouponId.HasValue)
            {
                coupon = await _unitOfWork.CouponRepository.GetByIdAsync(request.CouponId.Value);

                bool couponIsValid = coupon != null
                    && coupon.IsActive
                    && coupon.ExpiryDate > DateTime.Now
                    && (coupon.UsageLimit == null || coupon.UsedCount < coupon.UsageLimit);

                if (!couponIsValid)
                    return 0; // geçersiz kupon

                finalPrice = finalPrice - (finalPrice * coupon.DiscountPercentage / 100);
            }

            var ticket = _mapper.Map<Ticket>(request);
            ticket.PNRCode = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            ticket.PurchaseDate = DateTime.Now;
            ticket.Price = finalPrice;
            ticket.Status = "Active";
            ticket.IsEmailSent = false;

            eventEntity.RemainingCapacity -= 1;
            _unitOfWork.EventRepository.Update(eventEntity);

            if (coupon != null)
            {
                coupon.UsedCount += 1;
                _unitOfWork.CouponRepository.Update(coupon);
            }

            await _unitOfWork.TicketRepository.AddAsync(ticket);
            await _unitOfWork.SaveChangesAsync();

            // --- Bilet e-postası ---
            // Mail gönderimi, satın alma işleminin başarısını etkilemesin diye
            // ayrı bir try/catch içinde; mail atılamasa bile bilet geçerli kalır.
            try
            {
                var appUser = await _unitOfWork.AppUserRepository.GetByIdAsync(request.AppUserId);

                if (appUser != null && !string.IsNullOrEmpty(appUser.Email))
                {
                    await _emailService.SendTicketEmailAsync(new TicketEmailModel
                    {
                        RecipientEmail = appUser.Email,
                        CustomerFullName = $"{appUser.Name} {appUser.Surname}",
                        EventTitle = eventEntity.Title,
                        EventDate = eventEntity.EventDate,
                        Price = ticket.Price,
                        PNRCode = ticket.PNRCode,
                        Status = ticket.Status
                    }, cancellationToken);

                    ticket.IsEmailSent = true;
                    _unitOfWork.TicketRepository.Update(ticket);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch
            {
                // Mail gönderilemedi — ticket.IsEmailSent false kalır,
                // istenirse burada loglama eklenebilir.
            }

            return ticket.TicketId;
        }
    }
}