using AutoMapper;
using MediatR;
using TicketBox.Application.Features.CQRS.Coupons.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.Coupons.Handlers
{
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.CouponRepository.GetByIdAsync(request.CouponId);

            if (value == null)
                throw new Exception("Coupon bulunamadı.");

            _mapper.Map(request, value);

            _unitOfWork.CouponRepository.Update(value);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}