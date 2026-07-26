using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AuditLogs.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.AuditLogs.Handlers
{
    public class DeleteAuditLogCommandHandler : IRequestHandler<DeleteAuditLogCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuditLogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAuditLogCommand request, CancellationToken cancellationToken)
        {
            var log = await _unitOfWork.AuditLogRepository.GetByIdAsync(request.AuditLogId);
            if (log == null) return false;

            _unitOfWork.AuditLogRepository.Delete(log);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}