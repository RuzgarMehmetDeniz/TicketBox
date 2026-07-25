using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.AppUsers.Commands
{
    public class UpdateAppUserCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? PreferredCategories { get; set; }
    }
}
