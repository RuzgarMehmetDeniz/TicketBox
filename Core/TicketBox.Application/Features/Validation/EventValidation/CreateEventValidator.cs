using FluentValidation;
using TicketBox.Application.Features.CQRS.Events.Commands;

namespace TicketBox.Application.Validation.EventValidation
{
    public class CreateEventValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz.")
                .MaximumLength(2000).WithMessage("Açıklama en fazla 2000 karakter olabilir.");

            RuleFor(x => x.EventDate)
                .GreaterThan(DateTime.Now).WithMessage("Etkinlik tarihi gelecekte bir tarih olmalıdır.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Konum boş bırakılamaz.")
                .MaximumLength(300).WithMessage("Konum en fazla 300 karakter olabilir.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Kapasite 0'dan büyük olmalıdır.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");

            RuleFor(x => x.CreatedByUserId)
                .NotEmpty().WithMessage("Oluşturan kullanıcı bilgisi boş bırakılamaz.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmedi.");
        }
    }
}