using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.EventGalleries.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Handlers
{
    public class DeleteEventGalleryCommandHandler : IRequestHandler<DeleteEventGalleryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventGalleryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteEventGalleryCommand request, CancellationToken cancellationToken)
        {
            var gallery = await _unitOfWork.EventGalleryRepository.GetByIdAsync(request.EventGalleryId);
            if (gallery == null) return false;

            _unitOfWork.EventGalleryRepository.Delete(gallery);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}