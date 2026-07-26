using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Refunds.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Refunds.Handlers
{
    public class UpdateRefundCommandHandler : IRequestHandler<UpdateRefundCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRefundCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateRefundCommand request, CancellationToken cancellationToken)
        {
            var refund = await _unitOfWork.RefundRepository.GetByIdAsync(request.RefundId);
            if (refund == null) return false;

            refund.Status = request.Status;
            refund.ProcessedDate = DateTime.Now;

            // Onaylandıysa ilgili bileti de "Refunded" yap
            if (request.Status == "Onaylandı")
            {
                var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(refund.TicketId);
                if (ticket != null)
                {
                    ticket.Status = "Refunded";
                    _unitOfWork.TicketRepository.Update(ticket);
                }
            }

            _unitOfWork.RefundRepository.Update(refund);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}