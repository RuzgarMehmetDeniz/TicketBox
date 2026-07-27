using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminCategoryController : Controller
    {
        private readonly IMediator _mediator;

        public AdminCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ============ LİSTELEME ============
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return View(categories);
        }

        // ============ EKLEME - GET (form gösterir) ============
        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        // ============ EKLEME - POST ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(CategoryList));
        }

        // ============ GÜNCELLEME - GET (mevcut veriyi forma doldurur) ============
        [HttpGet]
        public async Task<IActionResult> CategoryUpdate(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = id });
            if (category == null)
                return NotFound();

            var command = new UpdateCategoryCommand
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                IconUrl = category.IconUrl,
                Description = category.Description,
                IsActive = category.IsActive
            };

            return View(command);
        }

        // ============ GÜNCELLEME - POST ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryUpdate(UpdateCategoryCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(CategoryList));
        }

        // ============ SİLME ============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand { CategoryId = id });
            return RedirectToAction(nameof(CategoryList));
        }
    }
}