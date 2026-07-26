using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Events.Queries;
using TicketBox.Application.Features.CQRS.Events.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationEvents;

namespace TicketBox.Application.Features.CQRS.Events.Handlers
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, GetEventByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetEventByIdQueryResult> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new EventWithCategorySpecification(request.EventId);
            var eventEntity = await _unitOfWork.EventRepository.GetEntityWithSpecAsync(spec);
            return _mapper.Map<GetEventByIdQueryResult>(eventEntity);
        }
    }
}