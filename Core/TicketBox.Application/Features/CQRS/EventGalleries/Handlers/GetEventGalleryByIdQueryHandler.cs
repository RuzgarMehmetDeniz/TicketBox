using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.EventGalleries.Queries;
using TicketBox.Application.Features.CQRS.EventGalleries.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Handlers
{
    public class GetEventGalleryByIdQueryHandler : IRequestHandler<GetEventGalleryByIdQuery, GetEventGalleryByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventGalleryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetEventGalleryByIdQueryResult> Handle(GetEventGalleryByIdQuery request, CancellationToken cancellationToken)
        {
            var gallery = await _unitOfWork.EventGalleryRepository.GetByIdAsync(request.EventGalleryId);
            return _mapper.Map<GetEventGalleryByIdQueryResult>(gallery);
        }
    }
}