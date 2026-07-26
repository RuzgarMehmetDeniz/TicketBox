using FluentValidation;
using TicketBox.Application.Features.CQRS.Reviews.Commands;

namespace TicketBox.Application.Validation.ReviewValidation
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewValidator()
        {
            RuleFor(x => x.ReviewId)
                .GreaterThan(0).WithMessage("Geçerli bir yorum seçilmedi.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Puan 1 ile 5 arasında olmalıdır.");

            RuleFor(x => x.Comment)
                .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir.");
        }
    }
}