using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Favorites.Commands;
using TicketBox.Application.Features.CQRS.Favorites.Queries;

namespace TicketBox.WebUI.Areas.Admin.Controllers
{
    public class AdminFavoriteController : Controller
    {
        private readonly IMediator _mediator;

        public AdminFavoriteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteList()
        {
            var values = await _mediator.Send(new GetAllFavoritesQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult FavoriteCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FavoriteCreate(CreateFavoriteCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var favoriteId = await _mediator.Send(command);

            if (favoriteId == 0)
            {
                ModelState.AddModelError("", "Bu etkinlik kullanıcı için zaten favorilerde ekli.");
                return View(command);
            }

            return RedirectToAction(nameof(FavoriteList));
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteUpdate(int id)
        {
            var value = await _mediator.Send(new GetFavoriteByIdQuery { FavoriteId = id });
            if (value == null)
                return NotFound();

            var command = new UpdateFavoriteCommand
            {
                FavoriteId = value.FavoriteId,
                AppUserId = value.AppUserId,
                EventId = value.EventId
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FavoriteUpdate(UpdateFavoriteCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(FavoriteList));

            ModelState.AddModelError("", "Güncelleme yapılırken bir hata oluştu.");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FavoriteDelete(int id)
        {
            await _mediator.Send(new DeleteFavoriteCommand { FavoriteId = id });
            return RedirectToAction(nameof(FavoriteList));
        }
    }
}