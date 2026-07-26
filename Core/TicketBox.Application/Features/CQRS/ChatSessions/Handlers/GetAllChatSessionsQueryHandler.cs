using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatSessions.Queries;
using TicketBox.Application.Features.CQRS.ChatSessions.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Handlers
{
    public class GetAllChatSessionsQueryHandler : IRequestHandler<GetAllChatSessionsQuery, List<GetChatSessionQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllChatSessionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetChatSessionQueryResult>> Handle(GetAllChatSessionsQuery request, CancellationToken cancellationToken)
        {
            var sessions = await _unitOfWork.ChatSessionRepository.GetAllAsync();
            return _mapper.Map<List<GetChatSessionQueryResult>>(sessions);
        }
    }
}