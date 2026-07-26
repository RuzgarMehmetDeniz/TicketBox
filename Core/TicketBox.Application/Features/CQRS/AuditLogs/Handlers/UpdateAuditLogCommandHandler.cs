using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AuditLogs.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.AuditLogs.Handlers
{
    public class UpdateAuditLogCommandHandler : IRequestHandler<UpdateAuditLogCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAuditLogCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAuditLogCommand request, CancellationToken cancellationToken)
        {
            var log = await _unitOfWork.AuditLogRepository.GetByIdAsync(request.AuditLogId);
            if (log == null) return false;

            _mapper.Map(request, log);

            _unitOfWork.AuditLogRepository.Update(log);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}