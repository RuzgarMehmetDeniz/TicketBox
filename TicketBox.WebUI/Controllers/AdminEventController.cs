using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Queries;
using TicketBox.Application.Features.CQRS.Events.Commands;
using TicketBox.Application.Features.CQRS.Events.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminEventController : Controller
    {
        private readonly IMediator _mediator;
        public AdminEventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private async Task PopulateCategoriesAsync(object? selectedId = null)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", selectedId);
        }

        // ============ LİSTELEME (SAYFALAMA İLE) ============
        [HttpGet]
        public async Task<IActionResult> EventList(int page = 1, int pageSize = 10)
        {
            var allEvents = await _mediator.Send(new GetAllEventsQuery());

            if (page < 1) page = 1;

            var totalCount = allEvents.Count;
            var totalPages = (int)System.Math.Ceiling(totalCount / (double)pageSize);
            if (totalPages < 1) totalPages = 1;
            if (page > totalPages) page = totalPages;

            var pagedEvents = allEvents
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;

            return View(pagedEvents);
        }

        // ============ EKLEME - GET ============
        [HttpGet]
        public async Task<IActionResult> EventCreate()
        {
            await PopulateCategoriesAsync();
            return View();
        }

        // ============ EKLEME - POST ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventCreate(CreateEventCommand command)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesAsync(command.CategoryId);
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(EventList));
        }

        // ============ GÜNCELLEME - GET ============
        [HttpGet]
        public async Task<IActionResult> EventUpdate(int id)
        {
            var ev = await _mediator.Send(new GetEventByIdQuery { EventId = id });
            if (ev == null)
                return NotFound();
            var command = new UpdateEventCommand
            {
                EventId = ev.EventId,
                Title = ev.Title,
                Description = ev.Description,
                EventDate = ev.EventDate,
                Location = ev.Location,
                Capacity = ev.Capacity,
                Price = ev.Price,
                ImageUrl = ev.ImageUrl,
                IsActive = ev.IsActive,
                CategoryId = ev.CategoryId
            };
            await PopulateCategoriesAsync(ev.CategoryId);
            return View(command);
        }

        // ============ GÜNCELLEME - POST ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventUpdate(UpdateEventCommand command)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesAsync(command.CategoryId);
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(EventList));
        }

        // ============ SİLME ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventDelete(int id)
        {
            await _mediator.Send(new DeleteEventCommand { EventId = id });
            return RedirectToAction(nameof(EventList));
        }
    }
}