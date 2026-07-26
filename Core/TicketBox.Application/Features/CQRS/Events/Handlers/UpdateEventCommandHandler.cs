using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Events.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Events.Handlers
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId);
            if (eventEntity == null) return false;

            // Kapasite değiştiyse, kalan kapasiteyi orantılı güncelle
            if (request.Capacity != eventEntity.Capacity)
            {
                var usedCapacity = eventEntity.Capacity - eventEntity.RemainingCapacity;
                eventEntity.RemainingCapacity = request.Capacity - usedCapacity;
            }

            _mapper.Map(request, eventEntity);

            _unitOfWork.EventRepository.Update(eventEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}