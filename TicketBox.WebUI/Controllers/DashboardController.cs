using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;
using TicketBox.WebUI.Models; // DashboardViewModel burada

namespace TicketBox.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // ---- Ham veriler ----
            var users = await _unitOfWork.AppUserRepository.GetAllAsync();
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var tickets = await _unitOfWork.TicketRepository.GetAllAsync();
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();
            var refunds = await _unitOfWork.RefundRepository.GetAllAsync();
            var payments = await _unitOfWork.PaymentRepository.GetAllAsync();
            var notifications = await _unitOfWork.NotificationRepository.GetAllAsync();
            var coupons = await _unitOfWork.CouponRepository.GetAllAsync();
            var auditLogs = await _unitOfWork.AuditLogRepository.GetAllAsync();
            var events = await _unitOfWork.EventRepository.GetAllAsync();

            var model = new DashboardViewModel
            {
                // Satır 1 — col-md-4 x 3
                RecentUsers = users
                    .OrderByDescending(u => u.CreatedDate)
                    .Take(5)
                    .ToList(),

                RecentCategories = categories
                    .OrderByDescending(c => c.CategoryId)
                    .Take(5)
                    .ToList(),

                RecentReservations = tickets
                    .OrderByDescending(t => t.PurchaseDate)
                    .Take(5)
                    .ToList(),

                // Satır 2 — col-md-6 x 2
                RecentTickets = tickets
                    .OrderByDescending(t => t.PurchaseDate)
                    .Take(6)
                    .ToList(),

                RecentReviews = reviews
                    .OrderByDescending(r => r.CreatedDate)
                    .Take(6)
                    .ToList(),

                // Satır 3 — col-md-4 x 3
                RecentRefunds = refunds
                    .OrderByDescending(r => r.RequestDate)
                    .Take(4)
                    .ToList(),

                RecentPayments = payments
                    .OrderByDescending(p => p.PaymentDate)
                    .Take(4)
                    .ToList(),

                RecentNotifications = notifications
                    .OrderByDescending(n => n.CreatedDate)
                    .Take(4)
                    .ToList(),

                // Satır 4 — col-md-6 x 2
                ActiveCoupons = coupons
                    .Where(c => c.IsActive && c.ExpiryDate >= DateTime.Now)
                    .OrderByDescending(c => c.ExpiryDate)
                    .Take(3)
                    .ToList(),

                RecentAuditLogs = auditLogs
                    .OrderByDescending(a => a.CreatedDate)
                    .Take(3)
                    .ToList(),

                // Satır 5 — col-md-12
                RecentEvents = events
                    .OrderByDescending(e => e.CreatedDate)
                    .Take(12)
                    .ToList(),

                // Özet kartlar için toplamlar
                TotalUsers = users.Count,
                TotalEvents = events.Count,
                TotalTickets = tickets.Count,
                TotalRevenue = payments.Where(p => p.Status == "Başarılı").Sum(p => p.Amount),
                PendingRefunds = refunds.Count(r => r.Status == "Beklemede")
            };

            // ---- Aylık gelir trendi (son 6 ay, Chart.js için) ----
            var last6Months = Enumerable.Range(0, 6)
                .Select(i => DateTime.Now.AddMonths(-i))
                .OrderBy(d => d)
                .ToList();

            model.MonthlyLabels = last6Months
                .Select(d => d.ToString("MMM yyyy"))
                .ToList();

            model.MonthlyRevenue = last6Months
                .Select(d => payments
                    .Where(p => p.Status == "Başarılı"
                        && p.PaymentDate.Year == d.Year
                        && p.PaymentDate.Month == d.Month)
                    .Sum(p => p.Amount))
                .ToList();

            return View(model);
        }
    }
}