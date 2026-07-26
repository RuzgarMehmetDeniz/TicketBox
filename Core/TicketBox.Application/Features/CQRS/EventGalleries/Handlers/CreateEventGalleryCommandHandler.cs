using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.EventGalleries.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Handlers
{
    public class CreateEventGalleryCommandHandler : IRequestHandler<CreateEventGalleryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEventGalleryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateEventGalleryCommand request, CancellationToken cancellationToken)
        {
            var gallery = _mapper.Map<EventGallery>(request);

            await _unitOfWork.EventGalleryRepository.AddAsync(gallery);
            await _unitOfWork.SaveChangesAsync();
            return gallery.EventGalleryId;
        }
    }
}