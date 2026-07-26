using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Payments.Queries;
using TicketBox.Application.Features.CQRS.Payments.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.Payments.Handlers
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, GetPaymentByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPaymentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetPaymentByIdQueryResult> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(request.PaymentId);
            return _mapper.Map<GetPaymentByIdQueryResult>(payment);
        }
    }
}