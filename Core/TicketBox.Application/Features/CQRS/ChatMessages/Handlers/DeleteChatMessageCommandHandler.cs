using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatMessages.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Handlers
{
    public class DeleteChatMessageCommandHandler : IRequestHandler<DeleteChatMessageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteChatMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteChatMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.ChatMessageRepository.GetByIdAsync(request.ChatMessageId);
            if (message == null) return false;

            _unitOfWork.ChatMessageRepository.Delete(message);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}