using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatMessages.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationChatMessages;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Queries
{
    public class GetChatMessagesBySessionQueryHandler : IRequestHandler<GetChatMessagesBySessionQuery, List<GetChatMessageQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetChatMessagesBySessionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetChatMessageQueryResult>> Handle(GetChatMessagesBySessionQuery request, CancellationToken cancellationToken)
        {
            var spec = new ChatMessagesBySessionSpecification(request.ChatSessionId);
            var messages = await _unitOfWork.ChatMessageRepository.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<GetChatMessageQueryResult>>(messages);
        }
    }
}