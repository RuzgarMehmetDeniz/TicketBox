using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Notifications.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.Notifications.Handlers
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Notification>(request);
            notification.CreatedDate = DateTime.Now;
            notification.IsRead = false;

            await _unitOfWork.NotificationRepository.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}