using FluentValidation;
using TicketBox.Application.Features.CQRS.Refunds.Commands;

namespace TicketBox.Application.Validation.RefundValidation
{
    public class CreateRefundValidator : AbstractValidator<CreateRefundCommand>
    {
        public CreateRefundValidator()
        {
            RuleFor(x => x.TicketId)
                .GreaterThan(0).WithMessage("Geçerli bir bilet seçilmedi.");

            RuleFor(x => x.PaymentId)
                .GreaterThan(0).WithMessage("Geçerli bir ödeme seçilmedi.");

            RuleFor(x => x.RefundAmount)
                .GreaterThan(0).WithMessage("İade tutarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("İptal sebebi boş bırakılamaz.")
                .MaximumLength(500).WithMessage("İptal sebebi en fazla 500 karakter olabilir.");
        }
    }
}