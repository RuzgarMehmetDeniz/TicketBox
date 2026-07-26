using MediatR;
using TicketBox.Application.Features.CQRS.Favorites.Results;

namespace TicketBox.Application.Features.CQRS.Favorites.Queries
{
    public class GetFavoriteByIdQuery : IRequest<GetFavoriteByIdQueryResult>
    {
        public int FavoriteId { get; set; }
    }
}