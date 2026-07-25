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
    public class GetAllAppUsersQueryHandler : IRequestHandler<GetAllAppUsersQuery, List<GetAppUserQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAppUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetAppUserQueryResult>> Handle(GetAllAppUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.AppUserRepository.GetAllAsync();
            return _mapper.Map<List<GetAppUserQueryResult>>(users);
        }
    }
}
