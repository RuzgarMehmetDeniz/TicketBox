using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // LİSTE
        public async Task<IActionResult> Index()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return View(categories);
        }

        // DETAY
        public async Task<IActionResult> Details(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = id });
            return View(category);
        }

        // EKLEME - GET
        public IActionResult Create()
        {
            return View();
        }

        // EKLEME - POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction("Index");
        }

        // GÜNCELLEME - GET
        public async Task<IActionResult> Update(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = id });
            var updateCommand = _mapper.Map<UpdateCategoryCommand>(category);
            return View(updateCommand);
        }

        // GÜNCELLEME - POST
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction("Index");
        }

        // SİLME
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand { CategoryId = id });
            return RedirectToAction("Index");
        }
    }
}