using FluentValidation;
using TicketBox.Application.Features.CQRS.Events.Commands;

namespace TicketBox.Application.Validation.EventValidation
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Geçerli bir etkinlik seçilmedi.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz.")
                .MaximumLength(2000).WithMessage("Açıklama en fazla 2000 karakter olabilir.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Konum boş bırakılamaz.")
                .MaximumLength(300).WithMessage("Konum en fazla 300 karakter olabilir.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Kapasite 0'dan büyük olmalıdır.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmedi.");
        }
    }
}