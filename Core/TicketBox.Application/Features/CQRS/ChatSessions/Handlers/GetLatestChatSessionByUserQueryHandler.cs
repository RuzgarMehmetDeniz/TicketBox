using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatSessions.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationChatSessions;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Queries
{
    public class GetLatestChatSessionByUserQueryHandler : IRequestHandler<GetLatestChatSessionByUserQuery, ChatSessionWithMessagesResult?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLatestChatSessionByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ChatSessionWithMessagesResult?> Handle(GetLatestChatSessionByUserQuery request, CancellationToken cancellationToken)
        {
            var spec = new ChatSessionLatestByUserSpecification(request.AppUserId);
            var session = await _unitOfWork.ChatSessionRepository.GetEntityWithSpecAsync(spec);
            if (session == null) return null;

            return _mapper.Map<ChatSessionWithMessagesResult>(session);
        }
    }
}