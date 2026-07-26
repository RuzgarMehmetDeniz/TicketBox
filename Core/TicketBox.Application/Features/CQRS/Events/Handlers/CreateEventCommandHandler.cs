using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Events.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.Events.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = _mapper.Map<Event>(request);
            eventEntity.CreatedDate = DateTime.Now;
            eventEntity.IsActive = true;
            eventEntity.RemainingCapacity = request.Capacity;

            await _unitOfWork.EventRepository.AddAsync(eventEntity);
            await _unitOfWork.SaveChangesAsync();
            return eventEntity.EventId;
        }
    }
}