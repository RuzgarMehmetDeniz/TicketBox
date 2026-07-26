using FluentValidation;
using TicketBox.Application.Features.CQRS.EventGalleries.Commands;

namespace TicketBox.Application.Validation.EventGalleryValidation
{
    public class CreateEventGalleryValidator : AbstractValidator<CreateEventGalleryCommand>
    {
        public CreateEventGalleryValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Geçerli bir etkinlik seçilmedi.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel URL boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Görsel URL en fazla 500 karakter olabilir.");
        }
    }
}