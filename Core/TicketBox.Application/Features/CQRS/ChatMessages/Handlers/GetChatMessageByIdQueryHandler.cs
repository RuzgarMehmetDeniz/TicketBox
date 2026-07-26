using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatMessages.Queries;
using TicketBox.Application.Features.CQRS.ChatMessages.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Handlers
{
    public class GetChatMessageByIdQueryHandler : IRequestHandler<GetChatMessageByIdQuery, GetChatMessageByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetChatMessageByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetChatMessageByIdQueryResult> Handle(GetChatMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.ChatMessageRepository.GetByIdAsync(request.ChatMessageId);
            return _mapper.Map<GetChatMessageByIdQueryResult>(message);
        }
    }
}