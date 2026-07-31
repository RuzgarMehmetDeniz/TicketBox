using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketBox.WebUI.Models
{
    public class ReservationViewModel
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int RemainingCapacity { get; set; }

        [Required(ErrorMessage = "Adet zorunludur.")]
        [Range(1, 999, ErrorMessage = "Geçerli bir adet giriniz.")]
        public int Quantity { get; set; } = 1;

        public string? CouponCode { get; set; }
        public List<CouponOptionViewModel> AvailableCoupons { get; set; } = new();

        [Required(ErrorMessage = "Ödeme yöntemi seçiniz.")]
        public string PaymentMethod { get; set; } = "Kredi Kartı";

        [Required(ErrorMessage = "Kart numarası zorunludur.")]
        [CreditCard(ErrorMessage = "Geçerli bir kart numarası giriniz.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Kart üzerindeki isim zorunludur.")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Son kullanma tarihi zorunludur.")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV zorunludur.")]
        [StringLength(4, MinimumLength = 3)]
        public string Cvv { get; set; }
    }

    public class CouponOptionViewModel
    {
        public string Code { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}