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
    public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAppUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.AppUserRepository.GetByIdAsync(request.Id);
            if (user == null) return false;

            _unitOfWork.AppUserRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
