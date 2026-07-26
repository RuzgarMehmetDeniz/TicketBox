using MediatR;

namespace TicketBox.Application.Features.CQRS.Favorites.Commands
{
    public class UpdateFavoriteCommand : IRequest<bool>
    {
        public int FavoriteId { get; set; }
        public string AppUserId { get; set; }
        public int EventId { get; set; }
    }
}