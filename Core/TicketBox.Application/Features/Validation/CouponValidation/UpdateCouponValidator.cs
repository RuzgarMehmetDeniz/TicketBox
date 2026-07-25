using FluentValidation;
using TicketBox.Application.Features.CQRS.Coupons.Commands;

namespace TicketBox.Application.Validation.CouponValidation
{
    public class UpdateCouponValidator : AbstractValidator<UpdateCouponCommand>
    {
        public UpdateCouponValidator()
        {
            RuleFor(x => x.CouponId)
                .GreaterThan(0)
                .WithMessage("Geçerli bir kupon seçiniz.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Kupon kodu boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Kupon kodu en az 3 karakter olmalıdır.")
                .MaximumLength(20).WithMessage("Kupon kodu en fazla 20 karakter olabilir.");

            RuleFor(x => x.DiscountPercentage)
                .GreaterThan(0).WithMessage("İndirim oranı 0'dan büyük olmalıdır.")
                .LessThanOrEqualTo(100).WithMessage("İndirim oranı en fazla %100 olabilir.");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Son kullanma tarihi bugünden ileri bir tarih olmalıdır.");

            RuleFor(x => x.UsageLimit)
                .NotNull().WithMessage("Kullanım limiti boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Kullanım limiti en az 1 olmalıdır.");

            RuleFor(x => x.UsedCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Kullanım sayısı negatif olamaz.");
        }
    }
}