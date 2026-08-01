using AutoMapper;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;
using TicketBox.Application.Features.CQRS.AppUsers.Results;
using TicketBox.Application.Features.CQRS.AuditLogs.Commands;
using TicketBox.Application.Features.CQRS.AuditLogs.Results;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Application.Features.CQRS.ChatMessages.Commands;
using TicketBox.Application.Features.CQRS.ChatMessages.Results;
using TicketBox.Application.Features.CQRS.ChatSessions.Commands;
using TicketBox.Application.Features.CQRS.ChatSessions.Results;
using TicketBox.Application.Features.CQRS.Coupons.Commands;
using TicketBox.Application.Features.CQRS.Coupons.Results;
using TicketBox.Application.Features.CQRS.EventGalleries.Commands;
using TicketBox.Application.Features.CQRS.EventGalleries.Results;
using TicketBox.Application.Features.CQRS.Events.Commands;
using TicketBox.Application.Features.CQRS.Events.Results;
using TicketBox.Application.Features.CQRS.Favorites.Commands;
using TicketBox.Application.Features.CQRS.Favorites.Results;
using TicketBox.Application.Features.CQRS.Notifications.Commands;
using TicketBox.Application.Features.CQRS.Notifications.Results;
using TicketBox.Application.Features.CQRS.Payments.Commands;
using TicketBox.Application.Features.CQRS.Payments.Results;
using TicketBox.Application.Features.CQRS.Refunds.Commands;
using TicketBox.Application.Features.CQRS.Refunds.Results;
using TicketBox.Application.Features.CQRS.Reviews.Commands;
using TicketBox.Application.Features.CQRS.Reviews.Results;
using TicketBox.Application.Features.CQRS.Tickets.Commands;
using TicketBox.Application.Features.CQRS.Tickets.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //// ===== CATEGORY =====
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<Category, GetCategoryQueryResult>().ReverseMap();
            CreateMap<Category, GetCategoryByIdQueryResult>().ReverseMap();
            CreateMap<GetCategoryByIdQueryResult, UpdateCategoryCommand>().ReverseMap();

            //// ===== APPUSER =====
            CreateMap<AppUser, GetAppUserByIdQueryResult>().ReverseMap();
            CreateMap<AppUser, GetAppUserQueryResult>().ReverseMap();       
            CreateMap<AppUser, UpdateAppUserCommand>().ReverseMap();       
            CreateMap<AppUser, RegisterAppUserCommand>().ReverseMap();

            //// ===== Coupon =====
            CreateMap<Coupon, GetCouponQueryResult>().ReverseMap();
            CreateMap<Coupon, GetCouponByIdQueryResult>().ReverseMap();
            CreateMap<Coupon, CreateCouponCommand>().ReverseMap();
            CreateMap<Coupon, UpdateCouponCommand>().ReverseMap();

            //// ===== AUDITLOG  =====
            CreateMap<AuditLog, CreateAuditLogCommand>().ReverseMap();
            CreateMap<AuditLog, UpdateAuditLogCommand>().ReverseMap();
            CreateMap<AuditLog, GetAuditLogQueryResult>().ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUser.UserName)).ReverseMap();

            CreateMap<AuditLog, GetAuditLogByIdQueryResult>().ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUser.UserName)).ReverseMap();

            //// ===== NOTIFICATION =====
            CreateMap<Notification, CreateNotificationCommand>().ReverseMap();
            CreateMap<Notification, UpdateNotificationCommand>().ReverseMap();
            CreateMap<Notification, GetNotificationQueryResult>().ReverseMap();
            CreateMap<Notification, GetNotificationByIdQueryResult>().ReverseMap();

            //// ===== CHATSESSION =====
            CreateMap<CreateChatSessionCommand, ChatSession>().ReverseMap();
            CreateMap<ChatSession, UpdateChatSessionCommand>().ReverseMap();
            CreateMap<ChatSession, GetChatSessionQueryResult>().ReverseMap();
            CreateMap<ChatSession, GetChatSessionByIdQueryResult>().ReverseMap();

            //// ===== CHAT (kullanıcı tarafı) =====
            CreateMap<ChatSession, ChatSessionWithMessagesResult>();
            CreateMap<ChatMessage, ChatMessageItem>();

            //// ===== CHATMESSAGE =====
            CreateMap<CreateChatMessageCommand, ChatMessage>().ReverseMap();
            CreateMap<ChatMessage, UpdateChatMessageCommand>().ReverseMap();
            CreateMap<ChatMessage, GetChatMessageQueryResult>().ReverseMap();
            CreateMap<ChatMessage, GetChatMessageByIdQueryResult>().ReverseMap();

            //// ===== EVENT =====
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, GetEventQueryResult>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName)).ReverseMap();
            CreateMap<Event, GetEventByIdQueryResult>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName)).ReverseMap();
            CreateMap<Event, GetEventDetailQueryResult>()
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
    .ForMember(dest => dest.OrganizerName, opt => opt.MapFrom(src => src.CreatedByUser.Name + " " + src.CreatedByUser.Surname))
    .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
        src.Reviews.Any() ? Math.Round(src.Reviews.Average(r => r.Rating), 1) : 0))
    .ForMember(dest => dest.FillPercentage, opt => opt.MapFrom(src =>
        src.Capacity == 0 ? 0 : (int)Math.Round((src.Capacity - src.RemainingCapacity) * 100.0 / src.Capacity)))
    .ForMember(dest => dest.GalleryImageUrls, opt => opt.MapFrom(src => src.Galleries.Select(g => g.ImageUrl).ToList()))
    .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews.OrderByDescending(r => r.CreatedDate)));

            CreateMap<Review, EventDetailReviewResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.Name + " " + src.AppUser.Surname));


            //// ===== TICKET =====
            CreateMap<Ticket , CreateTicketCommand>().ReverseMap();
            CreateMap<Ticket, UpdateTicketCommand>().ReverseMap();
            CreateMap<Ticket, GetTicketQueryResult>().ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title)).ReverseMap();
            CreateMap<Ticket, GetTicketByIdQueryResult>().ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title)).ReverseMap();

            //// ===== PAYMENT =====
            CreateMap<Payment,CreatePaymentCommand>().ReverseMap();
            CreateMap<Payment, UpdatePaymentCommand>().ReverseMap();
            CreateMap<Payment, GetPaymentQueryResult>().ForMember(dest => dest.PNRCode, opt => opt.MapFrom(src => src.Ticket.PNRCode)).ReverseMap();
            CreateMap<Payment, GetPaymentByIdQueryResult>().ForMember(dest => dest.PNRCode, opt => opt.MapFrom(src => src.Ticket.PNRCode)).ReverseMap();

            //// ===== REFUND =====
            CreateMap<CreateRefundCommand, Refund>().ReverseMap();
            CreateMap<Refund, GetRefundQueryResult>().ForMember(dest => dest.PNRCode, opt => opt.MapFrom(src => src.Ticket.PNRCode)).ReverseMap();
            CreateMap<Refund, GetRefundByIdQueryResult>().ForMember(dest => dest.PNRCode, opt => opt.MapFrom(src => src.Ticket.PNRCode)).ReverseMap();

            //// ===== REVIEW =====
            CreateMap<CreateReviewCommand, Review>().ReverseMap();
            CreateMap<Review, UpdateReviewCommand>().ReverseMap();
            CreateMap<Review, GetReviewQueryResult>().ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title)).ReverseMap();
            CreateMap<Review, GetReviewByIdQueryResult>().ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title)).ReverseMap();

            //// ===== EVENTGALLERY =====
            CreateMap<EventGallery,CreateEventGalleryCommand>().ReverseMap();
            CreateMap<EventGallery, UpdateEventGalleryCommand>().ReverseMap();
            CreateMap<EventGallery, GetEventGalleryQueryResult>().ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title)).ReverseMap();
            CreateMap<EventGallery, GetEventGalleryByIdQueryResult>().ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title)).ReverseMap();

            //// ===== FAVORITE =====
            CreateMap<Favorite,CreateFavoriteCommand>().ReverseMap();
            CreateMap<Favorite, UpdateFavoriteCommand>().ReverseMap();
            CreateMap<Favorite, GetFavoriteQueryResult>().ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title));
            CreateMap<Favorite, GetFavoriteByIdQueryResult>().ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title));
        }
    }
}
