using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.ChatSessions.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Handlers
{
    public class UpdateChatSessionCommandHandler : IRequestHandler<UpdateChatSessionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateChatSessionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateChatSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.ChatSessionRepository.GetByIdAsync(request.ChatSessionId);
            if (session == null) return false;

            _mapper.Map(request, session);

            _unitOfWork.ChatSessionRepository.Update(session);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}