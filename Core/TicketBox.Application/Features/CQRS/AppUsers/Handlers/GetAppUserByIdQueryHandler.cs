using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AppUsers.Queries;
using TicketBox.Application.Features.CQRS.AppUsers.Results;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.AppUsers.Handlers
{
    public class GetAppUserByIdQueryHandler : IRequestHandler<GetAppUserByIdQuery, GetAppUserByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAppUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetAppUserByIdQueryResult> Handle(GetAppUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.AppUserRepository.GetByIdAsync(request.Id);
            return _mapper.Map<GetAppUserByIdQueryResult>(user);
        }
    }
}
