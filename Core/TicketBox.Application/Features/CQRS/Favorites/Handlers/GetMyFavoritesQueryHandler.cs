using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Favorites.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationFavorites;

namespace TicketBox.Application.Features.CQRS.Favorites.Queries
{
    public class GetMyFavoritesQueryHandler : IRequestHandler<GetMyFavoritesQuery, List<GetFavoriteQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMyFavoritesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetFavoriteQueryResult>> Handle(GetMyFavoritesQuery request, CancellationToken cancellationToken)
        {
            var spec = new FavoriteByUserSpecification(request.AppUserId);
            var favorites = await _unitOfWork.FavoriteRepository.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<GetFavoriteQueryResult>>(favorites);
        }
    }
}