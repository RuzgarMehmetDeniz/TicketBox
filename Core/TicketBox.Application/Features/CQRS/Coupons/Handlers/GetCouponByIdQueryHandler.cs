using AutoMapper;
using MediatR;
using TicketBox.Application.Features.CQRS.Coupons.Queries;
using TicketBox.Application.Features.CQRS.Coupons.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Coupons.Handlers
{
    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, GetCouponByIdQueryResult?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCouponByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCouponByIdQueryResult?> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.CouponRepository.GetByIdAsync(request.CouponId);

            if (value == null)
                return null;

            return _mapper.Map<GetCouponByIdQueryResult>(value);
        }
    }
}