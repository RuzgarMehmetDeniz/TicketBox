using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationFavorites
{
    public class FavoriteByUserSpecification : BaseSpecification<Favorite>
    {
        public FavoriteByUserSpecification(string appUserId) : base(f => f.AppUserId == appUserId)
        {
            AddInclude(f => f.Event);
        }
    }
}