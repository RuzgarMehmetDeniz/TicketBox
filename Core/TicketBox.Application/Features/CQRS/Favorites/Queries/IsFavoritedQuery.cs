using MediatR;

namespace TicketBox.Application.Features.CQRS.Favorites.Queries
{
    public class IsFavoritedQuery : IRequest<int?>
    {
        public string AppUserId { get; set; }
        public int EventId { get; set; }
    }
}