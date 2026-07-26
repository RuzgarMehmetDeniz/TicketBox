using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Notifications.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Notifications.Handlers
{
    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNotificationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);
            if (notification == null) return false;

            _unitOfWork.NotificationRepository.Delete(notification);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}