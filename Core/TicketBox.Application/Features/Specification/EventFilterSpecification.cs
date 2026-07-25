using System;
using TicketBox.Domain.Entities; // Event entity'nin bulunduğu namespace'e göre değiştir

namespace TicketBox.Application.Features.Specification
{
    public class EventFilterSpecification : BaseSpecification<Event>
    {
        public EventFilterSpecification(int? categoryId, string? location, DateTime? startDate, DateTime? endDate, bool onlyActive = true)
        {
            // Kriterleri dinamik olarak birleştiriyoruz
            Criteria = e =>
                (!categoryId.HasValue || e.CategoryId == categoryId.Value) &&
                (string.IsNullOrEmpty(location) || e.Location.Contains(location)) &&
                (!startDate.HasValue || e.EventDate >= startDate.Value) &&
                (!endDate.HasValue || e.EventDate <= endDate.Value) &&
                (!onlyActive || e.IsActive);

            // İlişkili tabloları da getirmek istersen (örn. Category)
            AddInclude(e => e.Category);

            // Varsayılan sıralama: en yakın tarihli etkinlik önce
            ApplyOrderBy(e => e.EventDate);
        }
    }
}