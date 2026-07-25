using FluentValidation;
using TicketBox.Application.Features.CQRS.Coupons.Commands;

namespace TicketBox.Application.Validation.CouponValidation
{
    public class CreateCouponValidator : AbstractValidator<CreateCouponCommand>
    {
        public CreateCouponValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Kupon kodu boş bırakılamaz.")
                .MinimumLength(3)
                .MaximumLength(20);

            RuleFor(x => x.DiscountPercentage)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Son kullanma tarihi bugünden büyük olmalıdır.");

            RuleFor(x => x.UsageLimit)
                .GreaterThan(0)
                .When(x => x.UsageLimit.HasValue);

            RuleFor(x => x.UsedCount)
                .GreaterThanOrEqualTo(0);
        }
    }
}