using FluentValidation;
using TicketBox.Application.Features.CQRS.Tickets.Commands;

namespace TicketBox.Application.Validation.TicketValidation
{
    public class UpdateTicketValidator : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketValidator()
        {
            RuleFor(x => x.TicketId)
                .GreaterThan(0).WithMessage("Geçerli bir bilet seçilmedi.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Durum boş bırakılamaz.")
                .Must(s => s == "Active" || s == "Used" || s == "Cancelled" || s == "Refunded")
                .WithMessage("Durum sadece 'Active', 'Used', 'Cancelled' veya 'Refunded' olabilir.");
        }
    }
}