using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Favorites.Queries;
using TicketBox.Application.Features.CQRS.Favorites.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationFavorites;

namespace TicketBox.Application.Features.CQRS.Favorites.Handlers
{
    public class GetFavoriteByIdQueryHandler : IRequestHandler<GetFavoriteByIdQuery, GetFavoriteByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFavoriteByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetFavoriteByIdQueryResult> Handle(GetFavoriteByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new FavoriteWithEventSpecification(request.FavoriteId);
            var favorite = await _unitOfWork.FavoriteRepository.GetEntityWithSpecAsync(spec);
            return _mapper.Map<GetFavoriteByIdQueryResult>(favorite);
        }
    }
}