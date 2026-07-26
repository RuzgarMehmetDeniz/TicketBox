using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.EventGalleries.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Handlers
{
    public class UpdateEventGalleryCommandHandler : IRequestHandler<UpdateEventGalleryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEventGalleryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateEventGalleryCommand request, CancellationToken cancellationToken)
        {
            var gallery = await _unitOfWork.EventGalleryRepository.GetByIdAsync(request.EventGalleryId);
            if (gallery == null) return false;

            _mapper.Map(request, gallery);

            _unitOfWork.EventGalleryRepository.Update(gallery);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}