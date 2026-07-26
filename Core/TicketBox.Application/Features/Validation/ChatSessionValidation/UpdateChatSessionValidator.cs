using FluentValidation;
using TicketBox.Application.Features.CQRS.ChatSessions.Commands;

namespace TicketBox.Application.Validation.ChatSessionValidation
{
    public class UpdateChatSessionValidator : AbstractValidator<UpdateChatSessionCommand>
    {
        public UpdateChatSessionValidator()
        {
            RuleFor(x => x.ChatSessionId)
                .GreaterThan(0).WithMessage("Geçerli bir oturum seçilmedi.");

            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı bilgisi boş bırakılamaz.");
        }
    }
}