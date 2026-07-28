using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.EventGalleries.Commands;
using TicketBox.Application.Features.CQRS.EventGalleries.Queries;
using TicketBox.Application.Features.CQRS.Events.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminEventGalleryController : Controller
    {
        private readonly IMediator _mediator;

        public AdminEventGalleryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private async Task LoadEventsDropdownAsync()
        {
            var events = await _mediator.Send(new GetAllEventsQuery());
            ViewBag.EventList = events.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.EventId.ToString()
            }).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> EventGalleryList()
        {
            var values = await _mediator.Send(new GetAllEventGalleriesQuery());
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> EventGalleryCreate()
        {
            await LoadEventsDropdownAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventGalleryCreate(CreateEventGalleryCommand command)
        {
            if (!ModelState.IsValid)
            {
                await LoadEventsDropdownAsync();
                return View(command);
            }

            var galleryId = await _mediator.Send(command);
            if (galleryId > 0)
                return RedirectToAction(nameof(EventGalleryList));

            ModelState.AddModelError("", "Galeri görseli eklenirken bir hata oluştu.");
            await LoadEventsDropdownAsync();
            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> EventGalleryUpdate(int id)
        {
            var value = await _mediator.Send(new GetEventGalleryByIdQuery { EventGalleryId = id });
            if (value == null)
                return NotFound();

            var command = new UpdateEventGalleryCommand
            {
                EventGalleryId = value.EventGalleryId,
                ImageUrl = value.ImageUrl
            };

            ViewBag.EventTitle = value.EventTitle;
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventGalleryUpdate(UpdateEventGalleryCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(EventGalleryList));

            ModelState.AddModelError("", "Galeri görseli güncellenirken bir hata oluştu.");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventGalleryDelete(int id)
        {
            await _mediator.Send(new DeleteEventGalleryCommand { EventGalleryId = id });
            return RedirectToAction(nameof(EventGalleryList));
        }
    }
}