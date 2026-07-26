using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatSessions.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Handlers
{
    public class DeleteChatSessionCommandHandler : IRequestHandler<DeleteChatSessionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteChatSessionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteChatSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.ChatSessionRepository.GetByIdAsync(request.ChatSessionId);
            if (session == null) return false;

            _unitOfWork.ChatSessionRepository.Delete(session);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}