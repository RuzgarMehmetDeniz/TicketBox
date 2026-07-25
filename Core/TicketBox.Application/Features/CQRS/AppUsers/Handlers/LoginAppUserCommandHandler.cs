using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.AppUsers.Handlers
{
    public class LoginAppUserCommandHandler : IRequestHandler<LoginAppUserCommand, bool>
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginAppUserCommandHandler(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(LoginAppUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.UserName,
                request.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            return result.Succeeded;
        }
    }
}
