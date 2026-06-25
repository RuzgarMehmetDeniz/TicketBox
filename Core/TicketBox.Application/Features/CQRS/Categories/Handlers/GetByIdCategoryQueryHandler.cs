using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Queries;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class GetByIdCategoryQueryHandler
    {
        private readonly TicketContext _context;

        public GetByIdCategoryQueryHandler(TicketContext context)
        {
            _context = context;
        }
        public async Task<GetByIdCategoryQueryResult> Handle(GetCategoryByIdQuery query)
        {
            var value = await _context.Categories.Where(x => x.CategoryId == query.CategoryId)
                .Select(x => new GetByIdCategoryQueryResult
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName
                }).FirstOrDefaultAsync();
            return value;
        }
    }
}
