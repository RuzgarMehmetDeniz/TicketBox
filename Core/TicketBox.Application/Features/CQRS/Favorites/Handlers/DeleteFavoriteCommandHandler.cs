using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Favorites.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Favorites.Handlers
{
    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFavoriteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favorite = await _unitOfWork.FavoriteRepository.GetByIdAsync(request.FavoriteId);
            if (favorite == null) return false;

            _unitOfWork.FavoriteRepository.Delete(favorite);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}