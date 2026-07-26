using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatMessages.Queries;
using TicketBox.Application.Features.CQRS.ChatMessages.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Handlers
{
    public class GetAllChatMessagesQueryHandler : IRequestHandler<GetAllChatMessagesQuery, List<GetChatMessageQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllChatMessagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetChatMessageQueryResult>> Handle(GetAllChatMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _unitOfWork.ChatMessageRepository.GetAllAsync();
            return _mapper.Map<List<GetChatMessageQueryResult>>(messages);
        }
    }
}