using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Refunds.Queries;
using TicketBox.Application.Features.CQRS.Refunds.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Refunds.Handlers
{
    public class GetAllRefundsQueryHandler : IRequestHandler<GetAllRefundsQuery, List<GetRefundQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRefundsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetRefundQueryResult>> Handle(GetAllRefundsQuery request, CancellationToken cancellationToken)
        {
            var refunds = await _unitOfWork.RefundRepository.GetAllAsync();
            return _mapper.Map<List<GetRefundQueryResult>>(refunds);
        }
    }
}