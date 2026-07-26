using MediatR;

namespace TicketBox.Application.Features.CQRS.Favorites.Commands
{
    public class DeleteFavoriteCommand : IRequest<bool>
    {
        public int FavoriteId { get; set; }
    }
}