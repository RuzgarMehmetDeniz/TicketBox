using FluentValidation;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;

namespace TicketBox.Application.Validation.AppUserValidation
{
    public class LoginAppUserValidator : AbstractValidator<LoginAppUserCommand>
    {
        public LoginAppUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş bırakılamaz.");
        }
    }
}