using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.Favorites.Results;

namespace TicketBox.Application.Features.CQRS.Favorites.Queries
{
    public class GetMyFavoritesQuery : IRequest<List<GetFavoriteQueryResult>>
    {
        public string AppUserId { get; set; }
    }
}