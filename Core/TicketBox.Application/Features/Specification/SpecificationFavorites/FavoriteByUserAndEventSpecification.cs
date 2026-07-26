using TicketBox.Application.Features.Specification;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationFavorites
{
    public class FavoriteByUserAndEventSpecification : BaseSpecification<Favorite>
    {
        public FavoriteByUserAndEventSpecification(string appUserId, int eventId)
            : base(f => f.AppUserId == appUserId && f.EventId == eventId)
        {
        }
    }
}