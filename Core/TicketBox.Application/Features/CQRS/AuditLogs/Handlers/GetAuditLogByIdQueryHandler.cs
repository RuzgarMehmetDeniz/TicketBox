using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AuditLogs.Queries;
using TicketBox.Application.Features.CQRS.AuditLogs.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.AuditLogs.Handlers
{
    public class GetAuditLogByIdQueryHandler : IRequestHandler<GetAuditLogByIdQuery, GetAuditLogByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAuditLogByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetAuditLogByIdQueryResult> Handle(GetAuditLogByIdQuery request, CancellationToken cancellationToken)
        {
            var log = await _unitOfWork.AuditLogRepository.GetByIdAsync(request.AuditLogId);
            return _mapper.Map<GetAuditLogByIdQueryResult>(log);
        }
    }
}