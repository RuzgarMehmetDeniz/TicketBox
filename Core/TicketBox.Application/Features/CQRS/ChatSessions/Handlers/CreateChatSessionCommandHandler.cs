using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatSessions.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Handlers
{
    public class CreateChatSessionCommandHandler : IRequestHandler<CreateChatSessionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateChatSessionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateChatSessionCommand request, CancellationToken cancellationToken)
        {
            var session = _mapper.Map<ChatSession>(request);
            session.StartedDate = DateTime.Now;

            await _unitOfWork.ChatSessionRepository.AddAsync(session);
            await _unitOfWork.SaveChangesAsync();
            return session.ChatSessionId;
        }
    }
}