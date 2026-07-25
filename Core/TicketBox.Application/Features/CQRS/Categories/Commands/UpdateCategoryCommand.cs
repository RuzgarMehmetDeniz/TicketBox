using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Results;

namespace TicketBox.Application.Features.CQRS.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<CategoryResult>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string IconUrl { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
