using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Tickets.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.Tickets.Handlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            return ticket.TicketId;
        }
    }
}