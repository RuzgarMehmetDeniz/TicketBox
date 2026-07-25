using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;
using TicketBox.Application.Features.Repository;

namespace TicketBox.Application.Features.CQRS.AppUsers.Handlers
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAppUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.AppUserRepository.GetByIdAsync(request.Id);
            if (user == null) return false;

            _mapper.Map(request, user); // Var olan user'ın üzerine map'ler

            _unitOfWork.AppUserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
