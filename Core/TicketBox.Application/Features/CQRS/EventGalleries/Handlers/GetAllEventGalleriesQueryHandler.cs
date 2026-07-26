using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.EventGalleries.Queries;
using TicketBox.Application.Features.CQRS.EventGalleries.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Handlers
{
    public class GetAllEventGalleriesQueryHandler : IRequestHandler<GetAllEventGalleriesQuery, List<GetEventGalleryQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEventGalleriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetEventGalleryQueryResult>> Handle(GetAllEventGalleriesQuery request, CancellationToken cancellationToken)
        {
            var galleries = await _unitOfWork.EventGalleryRepository.GetAllAsync();
            return _mapper.Map<List<GetEventGalleryQueryResult>>(galleries);
        }
    }
}