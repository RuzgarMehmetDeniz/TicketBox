using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Favorites.Commands;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationFavorites;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.Favorites.Handlers
{
    public class CreateFavoriteCommandHandler : IRequestHandler<CreateFavoriteCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFavoriteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
        {
            var spec = new FavoriteByUserAndEventSpecification(request.AppUserId, request.EventId);
            var existing = await _unitOfWork.FavoriteRepository.GetEntityWithSpecAsync(spec);

            if (existing != null) return 0; // zaten favoride

            var favorite = _mapper.Map<Favorite>(request);

            await _unitOfWork.FavoriteRepository.AddAsync(favorite);
            await _unitOfWork.SaveChangesAsync();
            return favorite.FavoriteId;
        }
    }
}