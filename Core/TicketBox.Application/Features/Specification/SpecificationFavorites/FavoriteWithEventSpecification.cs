using TicketBox.Application.Features.Specification;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationFavorites
{
    public class FavoriteWithEventSpecification : BaseSpecification<Favorite>
    {
        public FavoriteWithEventSpecification()
        {
            AddInclude(f => f.Event);
        }

        public FavoriteWithEventSpecification(int favoriteId) : base()
        {
            Criteria = f => f.FavoriteId == favoriteId;
            AddInclude(f => f.Event);
        }
    }
}