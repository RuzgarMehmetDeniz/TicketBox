using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Tickets.Queries;
using TicketBox.Application.Features.CQRS.Tickets.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationTickets;

namespace TicketBox.Application.Features.CQRS.Tickets.Handlers
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, List<GetTicketQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTicketsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var spec = new TicketWithEventSpecification();
            var tickets = await _unitOfWork.TicketRepository.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<GetTicketQueryResult>>(tickets);
        }
    }
}