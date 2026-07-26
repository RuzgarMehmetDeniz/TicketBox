using MediatR;

namespace TicketBox.Application.Features.CQRS.Favorites.Commands
{
    public class CreateFavoriteCommand : IRequest<int>
    {
        public string AppUserId { get; set; }
        public int EventId { get; set; }
    }
}