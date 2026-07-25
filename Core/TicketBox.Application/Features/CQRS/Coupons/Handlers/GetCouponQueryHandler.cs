using AutoMapper;
using MediatR;
using TicketBox.Application.Features.CQRS.Coupons.Queries;
using TicketBox.Application.Features.CQRS.Coupons.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Coupons.Handlers
{
    public class GetCouponQueryHandler : IRequestHandler<GetCouponQuery, List<GetCouponQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCouponQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetCouponQueryResult>> Handle(GetCouponQuery request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.CouponRepository.GetAllAsync();

            return _mapper.Map<List<GetCouponQueryResult>>(values);
        }
    }
}