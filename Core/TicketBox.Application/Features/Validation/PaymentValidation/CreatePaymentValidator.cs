using FluentValidation;
using TicketBox.Application.Features.CQRS.Payments.Commands;

namespace TicketBox.Application.Validation.PaymentValidation
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidator()
        {
            RuleFor(x => x.TicketId)
                .GreaterThan(0).WithMessage("Geçerli bir bilet seçilmedi.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Ödeme yöntemi boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Ödeme yöntemi en fazla 50 karakter olabilir.");
        }
    }
}