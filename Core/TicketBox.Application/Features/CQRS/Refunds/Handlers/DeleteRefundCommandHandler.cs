using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Refunds.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Refunds.Handlers
{
    public class DeleteRefundCommandHandler : IRequestHandler<DeleteRefundCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRefundCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRefundCommand request, CancellationToken cancellationToken)
        {
            var refund = await _unitOfWork.RefundRepository.GetByIdAsync(request.RefundId);
            if (refund == null) return false;

            _unitOfWork.RefundRepository.Delete(refund);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}