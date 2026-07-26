using FluentValidation;
using TicketBox.Application.Features.CQRS.Tickets.Commands;

namespace TicketBox.Application.Validation.TicketValidation
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Geçerli bir etkinlik seçilmedi.");

            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı bilgisi boş bırakılamaz.");
        }
    }
}