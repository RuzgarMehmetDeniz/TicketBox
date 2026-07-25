using AutoMapper;
using MediatR;
using TicketBox.Application.Features.CQRS.Categories.Queries;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
                throw new KeyNotFoundException($"Category with id {request.CategoryId} not found.");

            return _mapper.Map<CategoryResult>(category);
        }
    }
}