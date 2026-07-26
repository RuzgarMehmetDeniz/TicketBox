using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Refunds.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.Refunds.Handlers
{
    public class CreateRefundCommandHandler : IRequestHandler<CreateRefundCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRefundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateRefundCommand request, CancellationToken cancellationToken)
        {
            var refund = _mapper.Map<Refund>(request);
            refund.Status = "Beklemede";
            refund.RequestDate = DateTime.Now;
            refund.ProcessedDate = null;

            await _unitOfWork.RefundRepository.AddAsync(refund);
            await _unitOfWork.SaveChangesAsync();
            return refund.RefundId;
        }
    }
}