using FluentValidation;
using TicketBox.Application.Features.CQRS.Favorites.Commands;

namespace TicketBox.Application.Validation.FavoriteValidation
{
    public class CreateFavoriteValidator : AbstractValidator<CreateFavoriteCommand>
    {
        public CreateFavoriteValidator()
        {
            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı bilgisi boş bırakılamaz.");

            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Geçerli bir etkinlik seçilmedi.");
        }
    }
}