using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; set; }
    }
}
