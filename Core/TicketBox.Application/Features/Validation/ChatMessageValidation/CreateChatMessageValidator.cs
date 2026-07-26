using FluentValidation;
using TicketBox.Application.Features.CQRS.ChatMessages.Commands;

namespace TicketBox.Application.Validation.ChatMessageValidation
{
    public class CreateChatMessageValidator : AbstractValidator<CreateChatMessageCommand>
    {
        public CreateChatMessageValidator()
        {
            RuleFor(x => x.ChatSessionId)
                .GreaterThan(0).WithMessage("Geçerli bir sohbet oturumu seçilmedi.");

            RuleFor(x => x.Sender)
                .NotEmpty().WithMessage("Gönderen bilgisi boş bırakılamaz.")
                .Must(s => s == "User" || s == "Bot").WithMessage("Gönderen sadece 'User' veya 'Bot' olabilir.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Mesaj içeriği boş bırakılamaz.")
                .MaximumLength(2000).WithMessage("Mesaj en fazla 2000 karakter olabilir.");
        }
    }
}