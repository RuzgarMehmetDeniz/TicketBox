using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Payments.Queries;
using TicketBox.Application.Features.CQRS.Payments.Results;
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Specification.SpecificationPayments;

namespace TicketBox.Application.Features.CQRS.Payments.Handlers
{
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, List<GetPaymentQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPaymentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetPaymentQueryResult>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var spec = new PaymentWithTicketSpecification();
            var payments = await _unitOfWork.PaymentRepository.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<GetPaymentQueryResult>>(payments);
        }
    }
}