using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Notifications.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Notifications.Handlers
{
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);
            if (notification == null) return false;

            _mapper.Map(request, notification);

            _unitOfWork.NotificationRepository.Update(notification);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}