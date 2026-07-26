using FluentValidation;
using TicketBox.Application.Features.CQRS.Refunds.Commands;

namespace TicketBox.Application.Validation.RefundValidation
{
    public class UpdateRefundValidator : AbstractValidator<UpdateRefundCommand>
    {
        public UpdateRefundValidator()
        {
            RuleFor(x => x.RefundId)
                .GreaterThan(0).WithMessage("Geçerli bir iade seçilmedi.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Durum boş bırakılamaz.")
                .Must(s => s == "Beklemede" || s == "Onaylandı" || s == "Reddedildi")
                .WithMessage("Durum sadece 'Beklemede', 'Onaylandı' veya 'Reddedildi' olabilir.");
        }
    }
}