using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatMessages.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Handlers
{
    public class UpdateChatMessageCommandHandler : IRequestHandler<UpdateChatMessageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateChatMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateChatMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.ChatMessageRepository.GetByIdAsync(request.ChatMessageId);
            if (message == null) return false;

            _mapper.Map(request, message);

            _unitOfWork.ChatMessageRepository.Update(message);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}