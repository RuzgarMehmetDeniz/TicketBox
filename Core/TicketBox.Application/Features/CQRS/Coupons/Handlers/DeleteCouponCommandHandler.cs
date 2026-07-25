using MediatR;
using TicketBox.Application.Features.CQRS.Coupons.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Coupons.Handlers
{
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCouponCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.CouponRepository.GetByIdAsync(request.CouponId);

            if (value == null)
                throw new Exception("Coupon bulunamadı.");

            _unitOfWork.CouponRepository.Delete(value);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}