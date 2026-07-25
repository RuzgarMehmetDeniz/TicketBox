using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;
using TicketBox.Application.Features.CQRS.AppUsers.Results;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Handlers;
using TicketBox.Application.Features.CQRS.Categories.Queries;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // ===== CATEGORY =====
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<Category, GetCategoryQueryResult>().ReverseMap();
            CreateMap<Category, GetCategoryByIdQueryResult>().ReverseMap();

             //===== APPUSER =====
            CreateMap<AppUser, GetAppUserByIdQueryResult>().ReverseMap();
            CreateMap<AppUser, GetAppUserQueryResult>().ReverseMap();       
            CreateMap<AppUser, UpdateAppUserCommand>().ReverseMap();       
            CreateMap<AppUser, RegisterAppUserCommand>().ReverseMap();       

            //// ===== AUDITLOG =====
            //CreateMap<AuditLog, AuditLogResult>().ReverseMap();
            //CreateMap<AuditLog, CreateAuditLogCommand>().ReverseMap();     
            //CreateMap<AuditLog, UpdateAuditLogCommand>().ReverseMap();     
            //CreateMap<AuditLog, GetAuditLogByIdResult>().ReverseMap();     

            //// ===== CHATMESSAGE =====
            //CreateMap<ChatMessage, ChatMessageResult>().ReverseMap();
            //CreateMap<ChatMessage, CreateChatMessageCommand>().ReverseMap();   
            //CreateMap<ChatMessage, UpdateChatMessageCommand>().ReverseMap();   
            //CreateMap<ChatMessage, GetChatMessageByIdResult>().ReverseMap();   

            //// ===== CHATSESSION =====
            //CreateMap<ChatSession, ChatSessionResult>().ReverseMap();
            //CreateMap<ChatSession, CreateChatSessionCommand>().ReverseMap();   
            //CreateMap<ChatSession, UpdateChatSessionCommand>().ReverseMap();   
            //CreateMap<ChatSession, GetChatSessionByIdResult>().ReverseMap();   

            //// ===== COUPON =====
            //CreateMap<Coupon, CouponResult>().ReverseMap();
            //CreateMap<Coupon, CreateCouponCommand>().ReverseMap();         
            //CreateMap<Coupon, UpdateCouponCommand>().ReverseMap();         
            //CreateMap<Coupon, GetCouponByIdResult>().ReverseMap();         

            //// ===== EVENT =====
            //CreateMap<Event, EventResult>()
            //    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            //    .ReverseMap()
            //    .ForMember(dest => dest.Category, opt => opt.Ignore());
            //CreateMap<Event, CreateEventCommand>().ReverseMap();           
            //CreateMap<Event, UpdateEventCommand>().ReverseMap();           
            //CreateMap<Event, GetEventByIdResult>().ReverseMap();           

            //// ===== EVENTGALLERY =====
            //CreateMap<EventGallery, EventGalleryResult>().ReverseMap();
            //CreateMap<EventGallery, CreateEventGalleryCommand>().ReverseMap();  
            //CreateMap<EventGallery, UpdateEventGalleryCommand>().ReverseMap();  
            //CreateMap<EventGallery, GetEventGalleryByIdResult>().ReverseMap();  

            //// ===== FAVORITE =====
            //CreateMap<Favorite, FavoriteResult>().ReverseMap();
            //CreateMap<Favorite, CreateFavoriteCommand>().ReverseMap();     
            //CreateMap<Favorite, GetFavoriteByIdResult>().ReverseMap();     

            //// ===== NOTIFICATION =====
            //CreateMap<Notification, NotificationResult>().ReverseMap();
            //CreateMap<Notification, CreateNotificationCommand>().ReverseMap(); 
            //CreateMap<Notification, UpdateNotificationCommand>().ReverseMap(); 
            //CreateMap<Notification, GetNotificationByIdResult>().ReverseMap(); 

            //// ===== PAYMENT =====
            //CreateMap<Payment, PaymentResult>().ReverseMap();
            //CreateMap<Payment, CreatePaymentCommand>().ReverseMap();       
            //CreateMap<Payment, UpdatePaymentCommand>().ReverseMap();       
            //CreateMap<Payment, GetPaymentByIdResult>().ReverseMap();       

            //// ===== REFUND =====
            //CreateMap<Refund, RefundResult>().ReverseMap();
            //CreateMap<Refund, CreateRefundCommand>().ReverseMap();         
            //CreateMap<Refund, UpdateRefundCommand>().ReverseMap();         
            //CreateMap<Refund, GetRefundByIdResult>().ReverseMap();         

            //// ===== REVIEW =====
            //CreateMap<Review, ReviewResult>().ReverseMap();
            //CreateMap<Review, CreateReviewCommand>().ReverseMap();         
            //CreateMap<Review, UpdateReviewCommand>().ReverseMap();         
            //CreateMap<Review, GetReviewByIdResult>().ReverseMap();         

            //// ===== TICKET =====
            //CreateMap<Ticket, TicketResult>().ReverseMap();
            //CreateMap<Ticket, CreateTicketCommand>().ReverseMap();         
            //CreateMap<Ticket, UpdateTicketCommand>().ReverseMap();         
            //CreateMap<Ticket, GetTicketByIdResult>().ReverseMap();         
        }
    }
}
