using FluentValidation;
using TicketBox.Application.Features.CQRS.EventGalleries.Commands;

namespace TicketBox.Application.Validation.EventGalleryValidation
{
    public class UpdateEventGalleryValidator : AbstractValidator<UpdateEventGalleryCommand>
    {
        public UpdateEventGalleryValidator()
        {
            RuleFor(x => x.EventGalleryId)
                .GreaterThan(0).WithMessage("Geçerli bir galeri kaydı seçilmedi.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel URL boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Görsel URL en fazla 500 karakter olabilir.");
        }
    }
}