using FluentValidation;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;

namespace TicketBox.Application.Validation.AppUserValidation
{
    public class RegisterAppUserValidator : AbstractValidator<RegisterAppUserCommand>
    {
        public RegisterAppUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
                .MaximumLength(30).WithMessage("Kullanıcı adı en fazla 30 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş bırakılamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Şifre en fazla 100 karakter olabilir.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$")
                .WithMessage("Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.");

            RuleFor(x => x.Age)
                .InclusiveBetween(13, 120)
                .WithMessage("Yaş 13 ile 120 arasında olmalıdır.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Şehir en az 2 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Şehir en fazla 100 karakter olabilir.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Ülke boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Ülke en az 2 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Ülke en fazla 100 karakter olabilir.");
        }
    }
}