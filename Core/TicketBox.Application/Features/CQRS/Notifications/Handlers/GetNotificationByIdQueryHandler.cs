using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Notifications.Queries;
using TicketBox.Application.Features.CQRS.Notifications.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Notifications.Handlers
{
    public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, GetNotificationByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNotificationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetNotificationByIdQueryResult> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);
            return _mapper.Map<GetNotificationByIdQueryResult>(notification);
        }
    }
}