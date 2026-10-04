using Application.Handlers.Quizs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/Quizs")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuizController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("HealthCheckup")]
        public async Task<IActionResult> GetHealthCheckup()
        {
            var result = await this._mediator.Send(new GetHealthCheckupQuery());
            return Ok(result);
        }
    }
}
