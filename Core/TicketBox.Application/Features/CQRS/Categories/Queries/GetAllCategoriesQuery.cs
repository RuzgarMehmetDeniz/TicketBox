using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Results;

namespace TicketBox.Application.Features.CQRS.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<GetCategoryQueryResult>>
    {
    }
}
