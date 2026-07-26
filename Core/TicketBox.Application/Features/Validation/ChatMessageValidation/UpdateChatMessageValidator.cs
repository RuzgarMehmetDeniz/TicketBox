using FluentValidation;
using TicketBox.Application.Features.CQRS.ChatMessages.Commands;

namespace TicketBox.Application.Validation.ChatMessageValidation
{
    public class UpdateChatMessageValidator : AbstractValidator<UpdateChatMessageCommand>
    {
        public UpdateChatMessageValidator()
        {
            RuleFor(x => x.ChatMessageId)
                .GreaterThan(0).WithMessage("Geçerli bir mesaj seçilmedi.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Mesaj içeriği boş bırakılamaz.")
                .MaximumLength(2000).WithMessage("Mesaj en fazla 2000 karakter olabilir.");
        }
    }
}