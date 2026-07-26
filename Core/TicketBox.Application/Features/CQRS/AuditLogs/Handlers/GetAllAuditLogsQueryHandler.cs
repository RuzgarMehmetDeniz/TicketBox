using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AuditLogs.Queries;
using TicketBox.Application.Features.CQRS.AuditLogs.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.AuditLogs.Handlers
{
    public class GetAllAuditLogsQueryHandler : IRequestHandler<GetAllAuditLogsQuery, List<GetAuditLogQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAuditLogsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetAuditLogQueryResult>> Handle(GetAllAuditLogsQuery request, CancellationToken cancellationToken)
        {
            var logs = await _unitOfWork.AuditLogRepository.GetAllAsync();
            return _mapper.Map<List<GetAuditLogQueryResult>>(logs);
        }
    }
}