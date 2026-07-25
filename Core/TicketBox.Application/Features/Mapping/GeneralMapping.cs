using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Handlers;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // ===== CATEGORY =====
            CreateMap<Category, CreateCategoryCommandHandler>().ReverseMap();
            CreateMap<Category, GetCategoryByIdQueryHandler>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommandHandler>().ReverseMap();
            CreateMap<Category, GetAllCategoriesQueryHandler>().ReverseMap();

            // ===== APPUSER =====
            //CreateMap<AppUser, AppUserResult>().ReverseMap();
            //CreateMap<AppUser, CreateAppUserCommand>().ReverseMap();       // yok, hata verecek
            //CreateMap<AppUser, UpdateAppUserCommand>().ReverseMap();       // yok, hata verecek
            //CreateMap<AppUser, GetAppUserByIdResult>().ReverseMap();       // yok, hata verecek

            //// ===== AUDITLOG =====
            //CreateMap<AuditLog, AuditLogResult>().ReverseMap();
            //CreateMap<AuditLog, CreateAuditLogCommand>().ReverseMap();     // yok, hata verecek
            //CreateMap<AuditLog, UpdateAuditLogCommand>().ReverseMap();     // yok, hata verecek
            //CreateMap<AuditLog, GetAuditLogByIdResult>().ReverseMap();     // yok, hata verecek

            //// ===== CHATMESSAGE =====
            //CreateMap<ChatMessage, ChatMessageResult>().ReverseMap();
            //CreateMap<ChatMessage, CreateChatMessageCommand>().ReverseMap();   // yok, hata verecek
            //CreateMap<ChatMessage, UpdateChatMessageCommand>().ReverseMap();   // yok, hata verecek
            //CreateMap<ChatMessage, GetChatMessageByIdResult>().ReverseMap();   // yok, hata verecek

            //// ===== CHATSESSION =====
            //CreateMap<ChatSession, ChatSessionResult>().ReverseMap();
            //CreateMap<ChatSession, CreateChatSessionCommand>().ReverseMap();   // yok, hata verecek
            //CreateMap<ChatSession, UpdateChatSessionCommand>().ReverseMap();   // yok, hata verecek
            //CreateMap<ChatSession, GetChatSessionByIdResult>().ReverseMap();   // yok, hata verecek

            //// ===== COUPON =====
            //CreateMap<Coupon, CouponResult>().ReverseMap();
            //CreateMap<Coupon, CreateCouponCommand>().ReverseMap();         // yok, hata verecek
            //CreateMap<Coupon, UpdateCouponCommand>().ReverseMap();         // yok, hata verecek
            //CreateMap<Coupon, GetCouponByIdResult>().ReverseMap();         // yok, hata verecek

            //// ===== EVENT =====
            //CreateMap<Event, EventResult>()
            //    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            //    .ReverseMap()
            //    .ForMember(dest => dest.Category, opt => opt.Ignore());
            //CreateMap<Event, CreateEventCommand>().ReverseMap();           // yok, hata verecek
            //CreateMap<Event, UpdateEventCommand>().ReverseMap();           // yok, hata verecek
            //CreateMap<Event, GetEventByIdResult>().ReverseMap();           // yok, hata verecek

            //// ===== EVENTGALLERY =====
            //CreateMap<EventGallery, EventGalleryResult>().ReverseMap();
            //CreateMap<EventGallery, CreateEventGalleryCommand>().ReverseMap();  // yok, hata verecek
            //CreateMap<EventGallery, UpdateEventGalleryCommand>().ReverseMap();  // yok, hata verecek
            //CreateMap<EventGallery, GetEventGalleryByIdResult>().ReverseMap();  // yok, hata verecek

            //// ===== FAVORITE =====
            //CreateMap<Favorite, FavoriteResult>().ReverseMap();
            //CreateMap<Favorite, CreateFavoriteCommand>().ReverseMap();     // yok, hata verecek
            //CreateMap<Favorite, GetFavoriteByIdResult>().ReverseMap();     // yok, hata verecek

            //// ===== NOTIFICATION =====
            //CreateMap<Notification, NotificationResult>().ReverseMap();
            //CreateMap<Notification, CreateNotificationCommand>().ReverseMap(); // yok, hata verecek
            //CreateMap<Notification, UpdateNotificationCommand>().ReverseMap(); // yok, hata verecek
            //CreateMap<Notification, GetNotificationByIdResult>().ReverseMap(); // yok, hata verecek

            //// ===== PAYMENT =====
            //CreateMap<Payment, PaymentResult>().ReverseMap();
            //CreateMap<Payment, CreatePaymentCommand>().ReverseMap();       // yok, hata verecek
            //CreateMap<Payment, UpdatePaymentCommand>().ReverseMap();       // yok, hata verecek
            //CreateMap<Payment, GetPaymentByIdResult>().ReverseMap();       // yok, hata verecek

            //// ===== REFUND =====
            //CreateMap<Refund, RefundResult>().ReverseMap();
            //CreateMap<Refund, CreateRefundCommand>().ReverseMap();         // yok, hata verecek
            //CreateMap<Refund, UpdateRefundCommand>().ReverseMap();         // yok, hata verecek
            //CreateMap<Refund, GetRefundByIdResult>().ReverseMap();         // yok, hata verecek

            //// ===== REVIEW =====
            //CreateMap<Review, ReviewResult>().ReverseMap();
            //CreateMap<Review, CreateReviewCommand>().ReverseMap();         // yok, hata verecek
            //CreateMap<Review, UpdateReviewCommand>().ReverseMap();         // yok, hata verecek
            //CreateMap<Review, GetReviewByIdResult>().ReverseMap();         // yok, hata verecek

            //// ===== TICKET =====
            //CreateMap<Ticket, TicketResult>().ReverseMap();
            //CreateMap<Ticket, CreateTicketCommand>().ReverseMap();         // yok, hata verecek
            //CreateMap<Ticket, UpdateTicketCommand>().ReverseMap();         // yok, hata verecek
            //CreateMap<Ticket, GetTicketByIdResult>().ReverseMap();         // yok, hata verecek
        }
    }
}
