using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Reviews.Queries;
using TicketBox.Application.Features.CQRS.Reviews.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationReviews;

namespace TicketBox.Application.Features.CQRS.Reviews.Handlers
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, List<GetReviewQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetReviewQueryResult>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ReviewWithEventSpecification();
            var reviews = await _unitOfWork.ReviewRepository.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<GetReviewQueryResult>>(reviews);
        }
    }
}