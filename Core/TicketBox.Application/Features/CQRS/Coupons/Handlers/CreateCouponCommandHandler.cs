using AutoMapper;
using MediatR;
using TicketBox.Application.Features.CQRS.Coupons.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.Coupons.Handlers
{
    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<Coupon>(request);

            await _unitOfWork.CouponRepository.AddAsync(value);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}