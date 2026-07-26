namespace TicketBox.Application.Features.CQRS.Favorites.Results
{
    public class GetFavoriteByIdQueryResult
    {
        public int FavoriteId { get; set; }
        public string AppUserId { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
    }
}