using FluentValidation;
using TicketBox.Application.Features.CQRS.Notifications.Commands;

namespace TicketBox.Application.Validation.NotificationValidation
{
    public class UpdateNotificationValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationValidator()
        {
            RuleFor(x => x.NotificationId)
                .GreaterThan(0).WithMessage("Geçerli bir bildirim seçilmedi.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj boş bırakılamaz.")
                .MaximumLength(1000).WithMessage("Mesaj en fazla 1000 karakter olabilir.");
        }
    }
}