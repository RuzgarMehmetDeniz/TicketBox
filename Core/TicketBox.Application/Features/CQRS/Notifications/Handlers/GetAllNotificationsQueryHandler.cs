using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Notifications.Queries;
using TicketBox.Application.Features.CQRS.Notifications.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Notifications.Handlers
{
    public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, List<GetNotificationQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllNotificationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetNotificationQueryResult>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _unitOfWork.NotificationRepository.GetAllAsync();
            return _mapper.Map<List<GetNotificationQueryResult>>(notifications);
        }
    }
}