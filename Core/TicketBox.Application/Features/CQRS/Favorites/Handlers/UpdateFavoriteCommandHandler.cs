using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Favorites.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Favorites.Handlers
{
    public class UpdateFavoriteCommandHandler : IRequestHandler<UpdateFavoriteCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFavoriteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favorite = await _unitOfWork.FavoriteRepository.GetByIdAsync(request.FavoriteId);
            if (favorite == null) return false;

            _mapper.Map(request, favorite);

            _unitOfWork.FavoriteRepository.Update(favorite);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}