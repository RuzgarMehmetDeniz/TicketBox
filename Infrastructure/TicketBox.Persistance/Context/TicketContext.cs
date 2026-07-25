using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TicketBox.Domain.Entities;

namespace TicketBox.Persistance.Context
{
    public class TicketContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NıTRO-AN515-57; initial Catalog = DbTicketSphere;TrustServerCertificate=True; integrated security=true");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EventGallery> EventGalleries { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // IdentityDbContext'in kendi konfigürasyonu için ZORUNLU

            // Tüm decimal property'ler için toplu precision ayarı
            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            // Favorite ilişkileri
            builder.Entity<Favorite>()
                .HasOne(f => f.AppUser)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Favorite>()
                .HasOne(f => f.Event)
                .WithMany(e => e.Favorites)
                .HasForeignKey(f => f.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review ilişkileri
            builder.Entity<Review>()
                .HasOne(r => r.AppUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Reviews)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket ilişkileri
            builder.Entity<Ticket>()
                .HasOne(t => t.AppUser)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket -> Coupon
            builder.Entity<Ticket>()
                .HasOne(t => t.Coupon)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CouponId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket -> Payment (1-1)
            builder.Entity<Payment>()
                .HasOne(p => p.Ticket)
                .WithOne(t => t.Payment)
                .HasForeignKey<Payment>(p => p.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            // Refund ilişkileri
            builder.Entity<Refund>()
                .HasOne(r => r.Ticket)
                .WithOne(t => t.Refund)
                .HasForeignKey<Refund>(r => r.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Refund>()
                .HasOne(r => r.Payment)
                .WithMany()
                .HasForeignKey(r => r.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Event -> CreatedByUser
            builder.Entity<Event>()
                .HasOne(e => e.CreatedByUser)
                .WithMany(u => u.CreatedEvents)
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ChatSession
            builder.Entity<ChatSession>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.ChatSessions)
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // AuditLog
            builder.Entity<AuditLog>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification
            builder.Entity<Notification>()
                .HasOne(n => n.AppUser)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}