using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Handlers;

namespace TicketBox.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;
        private readonly UpdateCategoryCommandHandler _updateCategoryCommandHandler;
        private readonly RemoveCategoryCommandHandler _removeCategoryCommandHandler;
        private readonly GetByIdCategoryQueryHandler _getByIdCategoryQueryHandler;
        private readonly GetCategoryQueryHandler _getCategoryQueryHandler;

        public CategoryController(CreateCategoryCommandHandler createCategoryCommandHandler, 
            UpdateCategoryCommandHandler updateCategoryCommandHandler, 
            RemoveCategoryCommandHandler removeCategoryCommandHandler, 
            GetByIdCategoryQueryHandler getByIdCategoryQueryHandler, 
            GetCategoryQueryHandler getCategoryQueryHandler)
        {
            _createCategoryCommandHandler = createCategoryCommandHandler;
            _updateCategoryCommandHandler = updateCategoryCommandHandler;
            _removeCategoryCommandHandler = removeCategoryCommandHandler;
            _getByIdCategoryQueryHandler = getByIdCategoryQueryHandler;
            _getCategoryQueryHandler = getCategoryQueryHandler;
        }

        public async Task <IActionResult > CategoryList()
        {
            var values = await _getCategoryQueryHandler.Handle();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand createCategoryCommand)
        {
            await _createCategoryCommandHandler.Handler(createCategoryCommand);
            return RedirectToAction("CategoryList");
        }
    }
}
