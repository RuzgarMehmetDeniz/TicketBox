using FluentValidation;
using TicketBox.Application.Features.CQRS.Payments.Commands;

namespace TicketBox.Application.Validation.PaymentValidation
{
    public class UpdatePaymentValidator : AbstractValidator<UpdatePaymentCommand>
    {
        public UpdatePaymentValidator()
        {
            RuleFor(x => x.PaymentId)
                .GreaterThan(0).WithMessage("Geçerli bir ödeme seçilmedi.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Durum boş bırakılamaz.")
                .Must(s => s == "Başarılı" || s == "Başarısız" || s == "Beklemede")
                .WithMessage("Durum sadece 'Başarılı', 'Başarısız' veya 'Beklemede' olabilir.");
        }
    }
}