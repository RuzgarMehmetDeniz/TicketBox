using System.Collections.Generic;
using TicketBox.Domain.Entities;

namespace TicketBox.WebUI.Models
{
    public class DashboardViewModel
    {
        // Satır 1 — col-md-4 x 3
        public List<AppUser> RecentUsers { get; set; }
        public List<Category> RecentCategories { get; set; }
        public List<Ticket> RecentReservations { get; set; }

        // Satır 2 — col-md-6 x 2
        public List<Ticket> RecentTickets { get; set; }
        public List<Review> RecentReviews { get; set; }

        // Satır 3 — col-md-4 x 3
        public List<Refund> RecentRefunds { get; set; }
        public List<Payment> RecentPayments { get; set; }
        public List<Notification> RecentNotifications { get; set; }

        // Satır 4 — col-md-6 x 2
        public List<Coupon> ActiveCoupons { get; set; }
        public List<AuditLog> RecentAuditLogs { get; set; }

        // Satır 5 — col-md-12
        public List<Event> RecentEvents { get; set; }

        // Üst özet kartlar
        public int TotalUsers { get; set; }
        public int TotalEvents { get; set; }
        public int TotalTickets { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PendingRefunds { get; set; }

        // Chart.js — aylık gelir trendi
        public List<string> MonthlyLabels { get; set; }
        public List<decimal> MonthlyRevenue { get; set; }
    }
}