using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Events.Queries;
using TicketBox.Application.Features.CQRS.Events.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationEvents;

namespace TicketBox.Application.Features.CQRS.Events.Handlers
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, GetEventDetailQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetEventDetailQueryResult?> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var spec = new EventDetailSpecification(request.EventId);
            var ev = await _unitOfWork.EventRepository.GetEntityWithSpecAsync(spec);

            if (ev == null) return null;

            return _mapper.Map<GetEventDetailQueryResult>(ev);
        }
    }
}
