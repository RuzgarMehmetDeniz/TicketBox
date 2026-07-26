using FluentValidation;
using TicketBox.Application.Features.CQRS.AuditLogs.Commands;

namespace TicketBox.Application.Validation.AuditLogValidation
{
    public class UpdateAuditLogValidator : AbstractValidator<UpdateAuditLogCommand>
    {
        public UpdateAuditLogValidator()
        {
            RuleFor(x => x.AuditLogId)
                .GreaterThan(0).WithMessage("Geçerli bir kayıt seçilmedi.");

            RuleFor(x => x.Action)
                .NotEmpty().WithMessage("İşlem (Action) alanı boş bırakılamaz.")
                .MaximumLength(200).WithMessage("İşlem açıklaması en fazla 200 karakter olabilir.");

            RuleFor(x => x.Details)
                .MaximumLength(1000).WithMessage("Detay alanı en fazla 1000 karakter olabilir.");
        }
    }
}