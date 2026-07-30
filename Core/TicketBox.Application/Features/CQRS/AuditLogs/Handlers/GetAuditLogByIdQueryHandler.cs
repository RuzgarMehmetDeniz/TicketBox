using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AuditLogs.Queries;
using TicketBox.Application.Features.CQRS.AuditLogs.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification;

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
            var spec = new AuditLogWithAppUserSpecification(request.AuditLogId);
            var log = await _unitOfWork.AuditLogRepository.GetEntityWithSpecAsync(spec);
            return _mapper.Map<GetAuditLogByIdQueryResult>(log);
        }
    }
}