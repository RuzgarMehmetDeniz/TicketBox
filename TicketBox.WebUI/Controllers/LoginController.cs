using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;

namespace TicketBox.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginAppUserCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);

            if (!result)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View(command);
            }

            return RedirectToAction("Index3", "Disvoer");
        }
    }
}