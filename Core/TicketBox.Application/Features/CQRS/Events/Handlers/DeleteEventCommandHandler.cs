using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Events.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Events.Handlers
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId);
            if (eventEntity == null) return false;

            _unitOfWork.EventRepository.Delete(eventEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}