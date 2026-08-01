using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatSessions.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationChatSessions;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Queries
{
    public class GetChatSessionsByUserQueryHandler : IRequestHandler<GetChatSessionsByUserQuery, List<ChatSessionWithMessagesResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetChatSessionsByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ChatSessionWithMessagesResult>> Handle(GetChatSessionsByUserQuery request, CancellationToken cancellationToken)
        {
            var spec = new ChatSessionsByUserSpecification(request.AppUserId);
            var sessions = await _unitOfWork.ChatSessionRepository.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<ChatSessionWithMessagesResult>>(sessions);
        }
    }
}