using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Services
{
    public interface ITabiliAnalyticsService
    {
        Task<decimal> GetTotalRevenueAsync(DateTime? from, DateTime? to);
        Task<List<BestSellingEventDto>> GetBestSellingEventsAsync(int top);
        Task<List<CategoryRevenueDto>> GetRevenueByCategoryAsync();
        Task<List<MonthlyTrendDto>> GetMonthlyTicketTrendAsync(int months);
        Task<RefundStatsDto> GetRefundStatsAsync();
        Task<int> GetNewUsersCountAsync(DateTime? from, DateTime? to);
        Task<List<CouponUsageDto>> GetCouponUsageAsync();
    }
}