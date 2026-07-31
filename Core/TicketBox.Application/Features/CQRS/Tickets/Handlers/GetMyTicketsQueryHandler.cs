using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Tickets.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationTickets;

namespace TicketBox.Application.Features.CQRS.Tickets.Queries
{
    public class GetMyTicketsQueryHandler : IRequestHandler<GetMyTicketsQuery, List<GetTicketQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMyTicketsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetMyTicketsQuery request, CancellationToken cancellationToken)
        {
            var spec = new TicketByUserSpecification(request.AppUserId);
            var tickets = await _unitOfWork.TicketRepository.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<GetTicketQueryResult>>(tickets);
        }
    }
}