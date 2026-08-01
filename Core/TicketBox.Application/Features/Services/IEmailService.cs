using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Services
{
    public interface IEmailService
    {
        Task SendTicketEmailAsync(TicketEmailModel model, CancellationToken cancellationToken = default);
    }
    public class TicketEmailModel
    {
        public string RecipientEmail { get; set; } = string.Empty;
        public string CustomerFullName { get; set; } = string.Empty;
        public string EventTitle { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
        public string PNRCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
