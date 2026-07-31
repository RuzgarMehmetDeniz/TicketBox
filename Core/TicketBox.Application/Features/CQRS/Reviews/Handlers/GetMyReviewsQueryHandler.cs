using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Reviews.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationReviews;

namespace TicketBox.Application.Features.CQRS.Reviews.Queries
{
    public class GetMyReviewsQueryHandler : IRequestHandler<GetMyReviewsQuery, List<GetReviewQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMyReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetReviewQueryResult>> Handle(GetMyReviewsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ReviewByUserSpecification(request.AppUserId);
            var reviews = await _unitOfWork.ReviewRepository.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<GetReviewQueryResult>>(reviews);
        }
    }
}