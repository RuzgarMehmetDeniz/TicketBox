using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AppUsers.Commands;
using TicketBox.Application.Features.CQRS.AppUsers.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminAppUserController : Controller
    {
        private readonly IMediator _mediator;

        public AdminAppUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AppUserList()
        {
            var values = await _mediator.Send(new GetAllAppUsersQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult AppUserCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppUserCreate(RegisterAppUserCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(AppUserList));

            ModelState.AddModelError("", "Kullanıcı oluşturulurken bir hata oluştu. Kullanıcı adı veya e-posta adresi kullanımda olabilir.");
            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> AppUserUpdate(string id)
        {
            var value = await _mediator.Send(new GetAppUserByIdQuery { Id = id });
            if (value == null)
                return NotFound();

            var command = new UpdateAppUserCommand
            {
                Id = value.Id,
                Name = value.Name,
                Surname = value.Surname,
                UserName = value.UserName,
                Age = value.Age,
                City = value.City,
                Country = value.Country,
                ProfileImageUrl = value.ProfileImageUrl,
                PreferredCategories = value.PreferredCategories
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppUserUpdate(UpdateAppUserCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(AppUserList));

            ModelState.AddModelError("", "Kullanıcı bilgileri güncellenirken bir hata oluştu.");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppUserDelete(string id)
        {
            await _mediator.Send(new DeleteAppUserCommand { Id = id });
            return RedirectToAction(nameof(AppUserList));
        }
    }
}