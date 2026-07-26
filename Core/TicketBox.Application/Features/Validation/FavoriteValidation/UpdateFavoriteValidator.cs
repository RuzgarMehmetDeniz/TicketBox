using FluentValidation;
using TicketBox.Application.Features.CQRS.Favorites.Commands;

namespace TicketBox.Application.Validation.FavoriteValidation
{
    public class UpdateFavoriteValidator : AbstractValidator<UpdateFavoriteCommand>
    {
        public UpdateFavoriteValidator()
        {
            RuleFor(x => x.FavoriteId)
                .GreaterThan(0).WithMessage("Geçerli bir favori seçilmedi.");

            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı bilgisi boş bırakılamaz.");

            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Geçerli bir etkinlik seçilmedi.");
        }
    }
}