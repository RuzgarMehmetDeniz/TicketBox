using FluentValidation;
using TicketBox.Application.Features.CQRS.Notifications.Commands;

namespace TicketBox.Application.Validation.NotificationValidation
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı bilgisi boş bırakılamaz.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Mesaj en fazla 1000 karakter olabilir.");
        }
    }
}