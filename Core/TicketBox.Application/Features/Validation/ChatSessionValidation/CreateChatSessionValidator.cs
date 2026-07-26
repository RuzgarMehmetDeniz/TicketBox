using FluentValidation;
using TicketBox.Application.Features.CQRS.ChatSessions.Commands;

namespace TicketBox.Application.Validation.ChatSessionValidation
{
    public class CreateChatSessionValidator : AbstractValidator<CreateChatSessionCommand>
    {
        public CreateChatSessionValidator()
        {
            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı bilgisi boş bırakılamaz.");
        }
    }
}