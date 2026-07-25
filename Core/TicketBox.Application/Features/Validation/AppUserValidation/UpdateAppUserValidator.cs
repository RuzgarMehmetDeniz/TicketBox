using FluentValidation;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;

namespace TicketBox.Application.Validation.AppUserValidation
{
    public class UpdateAppUserValidator : AbstractValidator<UpdateAppUserCommand>
    {
        public UpdateAppUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kullanıcı bilgisi bulunamadı.");

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

            RuleFor(x => x.Age)
                .InclusiveBetween(13, 120)
                .WithMessage("Yaş 13 ile 120 arasında olmalıdır.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Şehir en az 2 karakter olmalıdır.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Ülke boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Ülke en az 2 karakter olmalıdır.");

            RuleFor(x => x.ProfileImageUrl)
                .NotEmpty().WithMessage("Profil resmi boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Profil resmi bağlantısı en fazla 500 karakter olabilir.");

            RuleFor(x => x.PreferredCategories)
                .NotEmpty().WithMessage("Tercih edilen kategoriler boş bırakılamaz.")
                .MaximumLength(250).WithMessage("Tercih edilen kategoriler en fazla 250 karakter olabilir.");
        }
    }
}