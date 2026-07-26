using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Tickets.Queries;
using TicketBox.Application.Features.CQRS.Tickets.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationTickets;

namespace TicketBox.Application.Features.CQRS.Tickets.Handlers
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, GetTicketByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTicketByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetTicketByIdQueryResult> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new TicketWithEventSpecification(request.TicketId);
            var ticket = await _unitOfWork.TicketRepository.GetEntityWithSpecAsync(spec);
            return _mapper.Map<GetTicketByIdQueryResult>(ticket);
        }
    }
}