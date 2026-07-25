using MediatR;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
                return false;

            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}