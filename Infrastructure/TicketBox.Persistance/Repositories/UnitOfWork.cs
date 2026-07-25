using System.Threading.Tasks;
using TicketBox.Application.Features.Repository;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;

namespace TicketBox.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TicketContext _context;

        public UnitOfWork(TicketContext context)
        {
            _context = context;
            CategoryRepository = new GenericRepository<Category, int>(_context);
            EventRepository = new GenericRepository<Event, int>(_context);
            TicketRepository = new GenericRepository<Ticket, int>(_context);
            PaymentRepository = new GenericRepository<Payment, int>(_context);
            RefundRepository = new GenericRepository<Refund, int>(_context);
            CouponRepository = new GenericRepository<Coupon, int>(_context);
            ReviewRepository = new GenericRepository<Review, int>(_context);
            FavoriteRepository = new GenericRepository<Favorite, int>(_context);
            NotificationRepository = new GenericRepository<Notification, int>(_context);
            EventGalleryRepository = new GenericRepository<EventGallery, int>(_context);
            ChatSessionRepository = new GenericRepository<ChatSession, int>(_context);
            AppUserRepository = new GenericRepository<AppUser, string>(_context);
        }

        public IGenericRepository<Category, int> CategoryRepository { get; }
        public IGenericRepository<Event, int> EventRepository { get; }
        public IGenericRepository<Ticket, int> TicketRepository { get; }
        public IGenericRepository<Payment, int> PaymentRepository { get; }
        public IGenericRepository<Refund, int> RefundRepository { get; }
        public IGenericRepository<Coupon, int> CouponRepository { get; }
        public IGenericRepository<Review, int> ReviewRepository { get; }
        public IGenericRepository<Favorite, int> FavoriteRepository { get; }
        public IGenericRepository<Notification, int> NotificationRepository { get; }
        public IGenericRepository<EventGallery, int> EventGalleryRepository { get; }
        public IGenericRepository<ChatSession, int> ChatSessionRepository { get; }
        public IGenericRepository<AppUser, string> AppUserRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}