using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Events.Queries;
using TicketBox.Application.Features.CQRS.Events.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationEvents;

namespace TicketBox.Application.Features.CQRS.Events.Handlers
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, List<GetEventQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEventsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetEventQueryResult>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var spec = new EventWithCategorySpecification();
            var events = await _unitOfWork.EventRepository.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<GetEventQueryResult>>(events);
        }
    }
}