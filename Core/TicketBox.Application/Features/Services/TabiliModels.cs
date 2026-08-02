using System.Collections.Generic;

namespace TicketBox.Application.Features.Services
{
    public class BestSellingEventDto
    {
        public string EventTitle { get; set; }
        public int TicketCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class CategoryRevenueDto
    {
        public string CategoryName { get; set; }
        public int TicketCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class MonthlyTrendDto
    {
        public string Month { get; set; } // "2026-08"
        public int TicketCount { get; set; }
        public decimal Revenue { get; set; }
    }

    public class RefundStatsDto
    {
        public int TotalRequests { get; set; }
        public int Approved { get; set; }
        public int Pending { get; set; }
        public int Rejected { get; set; }
        public decimal TotalRefundedAmount { get; set; }
    }

    public class CouponUsageDto
    {
        public string Code { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int UsedCount { get; set; }
        public int? UsageLimit { get; set; }
    }
}