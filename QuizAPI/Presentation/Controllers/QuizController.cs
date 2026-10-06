using Application.Dtos.Requests;
using Application.Handlers.Quizs.Commands;
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

        [HttpPost]
        public async Task<IActionResult> CreateQuiz(QuizRequestDto quizRequestDto)
        {
            var result = await this._mediator.Send(new CreateQuizCommand(quizRequestDto));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetQuizs()
        {
            var result = await this._mediator.Send(new GetQuizQuery());
            return Ok(result);
        }

        [HttpGet("{quizId}")]
        public async Task<IActionResult> GetQuizDetail([FromRoute] int quizId)
        {
            var result = await this._mediator.Send(new GetQuizDetailQuery(quizId));
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateQuizDetail(QuizUpdateRequestDto requestDto)
        {
            var result = await this._mediator.Send(new UpdateQuizCommand(requestDto));
            return Ok(result);
        }
    }
}
