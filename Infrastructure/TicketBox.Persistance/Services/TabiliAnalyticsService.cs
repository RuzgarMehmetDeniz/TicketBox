using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Services;
using TicketBox.Application.Features.Specification.SpecificationRefunds;
using TicketBox.Application.Features.Specification.SpecificationTickets;

namespace TicketBox.Persistance.Services
{
    public class TabiliAnalyticsService : ITabiliAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TabiliAnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime? from, DateTime? to)
        {
            var payments = await _unitOfWork.PaymentRepository.GetAllAsync();

            var query = payments.Where(p => p.Status == "Başarılı");
            if (from.HasValue) query = query.Where(p => p.PaymentDate >= from.Value);
            if (to.HasValue) query = query.Where(p => p.PaymentDate <= to.Value);

            return query.Sum(p => p.Amount);
        }

        public async Task<List<BestSellingEventDto>> GetBestSellingEventsAsync(int top)
        {
            var spec = new TicketWithEventSpecification();
            var tickets = await _unitOfWork.TicketRepository.GetAllWithSpecAsync(spec);

            return tickets
                .Where(t => t.Status != "Refunded")
                .GroupBy(t => t.Event.Title)
                .Select(g => new BestSellingEventDto
                {
                    EventTitle = g.Key,
                    TicketCount = g.Count(),
                    TotalRevenue = g.Sum(t => t.Price)
                })
                .OrderByDescending(x => x.TicketCount)
                .Take(top)
                .ToList();
        }

        public async Task<List<CategoryRevenueDto>> GetRevenueByCategoryAsync()
        {
            var spec = new TicketWithEventSpecification();
            var tickets = await _unitOfWork.TicketRepository.GetAllWithSpecAsync(spec);

            return tickets
                .Where(t => t.Status != "Refunded")
                .GroupBy(t => t.Event.Category.CategoryName)
                .Select(g => new CategoryRevenueDto
                {
                    CategoryName = g.Key,
                    TicketCount = g.Count(),
                    TotalRevenue = g.Sum(t => t.Price)
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToList();
        }

        public async Task<List<MonthlyTrendDto>> GetMonthlyTicketTrendAsync(int months)
        {
            var spec = new TicketWithEventSpecification();
            var tickets = await _unitOfWork.TicketRepository.GetAllWithSpecAsync(spec);

            var cutoff = DateTime.Now.AddMonths(-months);

            return tickets
                .Where(t => t.PurchaseDate >= cutoff && t.Status != "Refunded")
                .GroupBy(t => new { t.PurchaseDate.Year, t.PurchaseDate.Month })
                .Select(g => new MonthlyTrendDto
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                    TicketCount = g.Count(),
                    Revenue = g.Sum(t => t.Price)
                })
                .OrderBy(x => x.Month)
                .ToList();
        }

        public async Task<RefundStatsDto> GetRefundStatsAsync()
        {
            var spec = new RefundWithTicketSpecification();
            var refunds = await _unitOfWork.RefundRepository.GetAllWithSpecAsync(spec);

            return new RefundStatsDto
            {
                TotalRequests = refunds.Count,
                Approved = refunds.Count(r => r.Status == "Onaylandı"),
                Pending = refunds.Count(r => r.Status == "Beklemede"),
                Rejected = refunds.Count(r => r.Status == "Reddedildi"),
                TotalRefundedAmount = refunds.Where(r => r.Status == "Onaylandı").Sum(r => r.RefundAmount)
            };
        }

        public async Task<int> GetNewUsersCountAsync(DateTime? from, DateTime? to)
        {
            var users = await _unitOfWork.AppUserRepository.GetAllAsync();

            var query = users.AsEnumerable();
            if (from.HasValue) query = query.Where(u => u.CreatedDate >= from.Value);
            if (to.HasValue) query = query.Where(u => u.CreatedDate <= to.Value);

            return query.Count();
        }

        public async Task<List<CouponUsageDto>> GetCouponUsageAsync()
        {
            var coupons = await _unitOfWork.CouponRepository.GetAllAsync();

            return coupons
                .OrderByDescending(c => c.UsedCount)
                .Select(c => new CouponUsageDto
                {
                    Code = c.Code,
                    DiscountPercentage = c.DiscountPercentage,
                    UsedCount = c.UsedCount,
                    UsageLimit = c.UsageLimit
                })
                .ToList();
        }
    }
}