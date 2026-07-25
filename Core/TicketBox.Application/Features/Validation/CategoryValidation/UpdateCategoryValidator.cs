using FluentValidation;
using TicketBox.Application.Features.CQRS.Categories.Commands;

namespace TicketBox.Application.Validation.CategoryValidation
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Geçerli bir kategori seçiniz.");

            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.")
                .Matches(@"^(?=.*[A-Za-zÇçĞğİıÖöŞşÜü])[A-Za-zÇçĞğİıÖöŞşÜü0-9\s]+$")
                .WithMessage("Kategori adı en az bir harf içermelidir.");

            RuleFor(x => x.IconUrl)
                .NotEmpty().WithMessage("İkon URL alanı boş bırakılamaz.")
                .MaximumLength(500).WithMessage("İkon URL en fazla 500 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Kategori açıklaması boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Kategori açıklaması en az 10 karakter olmalıdır.")
                .MaximumLength(500).WithMessage("Kategori açıklaması en fazla 500 karakter olabilir.");
        }
    }
}