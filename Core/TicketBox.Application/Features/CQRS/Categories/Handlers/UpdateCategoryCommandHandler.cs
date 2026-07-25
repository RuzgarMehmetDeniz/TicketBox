using AutoMapper;
using MediatR;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
                throw new KeyNotFoundException($"Category with id {request.CategoryId} not found.");

            _mapper.Map(request, category); // request'teki alanları mevcut category nesnesine eşler

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryResult>(category);
        }
    }
}