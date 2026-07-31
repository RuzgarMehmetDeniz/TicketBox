using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationFavorites;

namespace TicketBox.Application.Features.CQRS.Favorites.Queries
{
    public class IsFavoritedQueryHandler : IRequestHandler<IsFavoritedQuery, int?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public IsFavoritedQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int?> Handle(IsFavoritedQuery request, CancellationToken cancellationToken)
        {
            var spec = new FavoriteByUserAndEventSpecification(request.AppUserId, request.EventId);
            var favorite = await _unitOfWork.FavoriteRepository.GetEntityWithSpecAsync(spec);
            return favorite?.FavoriteId;
        }
    }
}