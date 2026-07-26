using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Reviews.Queries;
using TicketBox.Application.Features.CQRS.Reviews.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationReviews;

namespace TicketBox.Application.Features.CQRS.Reviews.Handlers
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, GetReviewByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReviewByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetReviewByIdQueryResult> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new ReviewWithEventSpecification(request.ReviewId);
            var review = await _unitOfWork.ReviewRepository.GetEntityWithSpecAsync(spec);
            return _mapper.Map<GetReviewByIdQueryResult>(review);
        }
    }
}