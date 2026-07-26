using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatMessages.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Handlers
{
    public class CreateChatMessageCommandHandler : IRequestHandler<CreateChatMessageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateChatMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateChatMessageCommand request, CancellationToken cancellationToken)
        {
            var message = _mapper.Map<ChatMessage>(request);
            message.SentDate = DateTime.Now;

            await _unitOfWork.ChatMessageRepository.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}