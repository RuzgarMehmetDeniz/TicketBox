using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Category, int> CategoryRepository { get; }
        IGenericRepository<Event, int> EventRepository { get; }
        IGenericRepository<Ticket, int> TicketRepository { get; }
        IGenericRepository<Payment, int> PaymentRepository { get; }
        IGenericRepository<Refund, int> RefundRepository { get; }
        IGenericRepository<Coupon, int> CouponRepository { get; }
        IGenericRepository<Review, int> ReviewRepository { get; }
        IGenericRepository<Favorite, int> FavoriteRepository { get; }
        IGenericRepository<Notification, int> NotificationRepository { get; }
        IGenericRepository<EventGallery, int> EventGalleryRepository { get; }
        IGenericRepository<ChatSession, int> ChatSessionRepository { get; }
        IGenericRepository<AppUser, string> AppUserRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
