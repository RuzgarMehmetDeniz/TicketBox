using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AuditLogs.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.AuditLogs.Handlers
{
    public class CreateAuditLogCommandHandler : IRequestHandler<CreateAuditLogCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAuditLogCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateAuditLogCommand request, CancellationToken cancellationToken)
        {
            var log = _mapper.Map<AuditLog>(request);
            log.CreatedDate = DateTime.Now;

            await _unitOfWork.AuditLogRepository.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}