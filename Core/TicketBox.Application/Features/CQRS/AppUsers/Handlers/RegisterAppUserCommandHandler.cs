using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.AppUsers.Handlers
{
    public class RegisterAppUserCommandHandler : IRequestHandler<RegisterAppUserCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public RegisterAppUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> Handle(RegisterAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AppUser>(request);
            user.UserName = request.UserName;
            user.CreatedDate = DateTime.UtcNow;
            var result = await _userManager.CreateAsync(user, request.Password);
            return result.Succeeded;
        }
    }
}