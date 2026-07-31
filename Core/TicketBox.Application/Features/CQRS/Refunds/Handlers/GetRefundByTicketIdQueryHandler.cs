using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Refunds.Queries;
using TicketBox.Application.Features.CQRS.Refunds.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationRefunds;

namespace TicketBox.Application.Features.CQRS.Refunds.Handlers
{
    public class GetRefundByTicketIdQueryHandler : IRequestHandler<GetRefundByTicketIdQuery, GetRefundByIdQueryResult?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRefundByTicketIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetRefundByIdQueryResult?> Handle(GetRefundByTicketIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new RefundByTicketSpecification(request.TicketId);
            var refund = await _unitOfWork.RefundRepository.GetEntityWithSpecAsync(spec);
            if (refund == null) return null;
            return _mapper.Map<GetRefundByIdQueryResult>(refund);
        }
    }
}