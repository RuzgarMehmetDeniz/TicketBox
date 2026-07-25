using FluentValidation;
using TicketBox.Application.Features.CQRS.Categories.Commands;

namespace TicketBox.Application.Validation.CategoryValidation
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.")
                .Matches(@"[A-Za-zÇçĞğİıÖöŞşÜü]")
                .WithMessage("Kategori adı en az bir harf içermelidir.");

            RuleFor(x => x.IconUrl)
                .NotEmpty().WithMessage("İkon URL alanı boş bırakılamaz.")
                .MaximumLength(500).WithMessage("İkon URL en fazla 500 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}