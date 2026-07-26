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
    public class GetRefundByIdQueryHandler : IRequestHandler<GetRefundByIdQuery, GetRefundByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRefundByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetRefundByIdQueryResult> Handle(GetRefundByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new RefundWithTicketSpecification(request.RefundId);
            var refund = await _unitOfWork.RefundRepository.GetEntityWithSpecAsync(spec);
            return _mapper.Map<GetRefundByIdQueryResult>(refund);
        }
    }
}