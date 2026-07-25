using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.AppUsers.Commands
{
    public class DeleteAppUserCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
