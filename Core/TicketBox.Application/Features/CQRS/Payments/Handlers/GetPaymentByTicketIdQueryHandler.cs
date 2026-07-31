using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Payments.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationPayments;

namespace TicketBox.Application.Features.CQRS.Payments.Queries
{
    public class GetPaymentByTicketIdQueryHandler : IRequestHandler<GetPaymentByTicketIdQuery, GetPaymentQueryResult?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPaymentByTicketIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetPaymentQueryResult?> Handle(GetPaymentByTicketIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new PaymentByTicketSpecification(request.TicketId);
            var payment = await _unitOfWork.PaymentRepository.GetEntityWithSpecAsync(spec);
            if (payment == null) return null;

            return _mapper.Map<GetPaymentQueryResult>(payment);
        }
    }
}