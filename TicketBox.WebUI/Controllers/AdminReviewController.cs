using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Reviews.Commands;
using TicketBox.Application.Features.CQRS.Reviews.Queries;

namespace TicketBox.WebUI.Controllers
{
    public class AdminReviewController : Controller
    {
        private readonly IMediator _mediator;

        public AdminReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ReviewList()
        {
            var values = await _mediator.Send(new GetAllReviewsQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult ReviewCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewCreate(CreateReviewCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(ReviewList));
        }

        [HttpGet]
        public async Task<IActionResult> ReviewUpdate(int id)
        {
            var value = await _mediator.Send(new GetReviewByIdQuery { ReviewId = id });
            if (value == null)
                return NotFound();

            var command = new UpdateReviewCommand
            {
                ReviewId = value.ReviewId,
                Rating = value.Rating,
                Comment = value.Comment
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewUpdate(UpdateReviewCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction(nameof(ReviewList));

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewDelete(int id)
        {
            await _mediator.Send(new DeleteReviewCommand { ReviewId = id });
            return RedirectToAction(nameof(ReviewList));
        }
    }
}