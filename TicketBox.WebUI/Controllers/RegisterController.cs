using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;

namespace TicketBox.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterAppUserCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);

            if (!result)
            {
                ModelState.AddModelError("", "Kayıt başarısız oldu. Bilgilerinizi kontrol edin.");
                return View(command);
            }
            return RedirectToAction("Index", "Login");
        }
    }
}