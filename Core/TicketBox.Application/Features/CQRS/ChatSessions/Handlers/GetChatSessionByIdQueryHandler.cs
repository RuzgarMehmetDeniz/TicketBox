using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatSessions.Queries;
using TicketBox.Application.Features.CQRS.ChatSessions.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Handlers
{
    public class GetChatSessionByIdQueryHandler : IRequestHandler<GetChatSessionByIdQuery, GetChatSessionByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetChatSessionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetChatSessionByIdQueryResult> Handle(GetChatSessionByIdQuery request, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.ChatSessionRepository.GetByIdAsync(request.ChatSessionId);
            return _mapper.Map<GetChatSessionByIdQueryResult>(session);
        }
    }
}