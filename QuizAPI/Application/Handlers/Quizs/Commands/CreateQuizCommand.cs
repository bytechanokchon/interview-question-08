using Application.Dtos.Requests;
using Application.Dtos.Services;
using Application.Dtos.Shareds;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Handlers.Quizs.Commands
{
    public class CreateQuizCommand : IRequest<BaseResponseDto<object>>
    {
        public CreateQuizCommand(QuizRequestDto quizRequestDto)
        {
            this.QuizRequestDto = quizRequestDto;
        }

        public QuizRequestDto QuizRequestDto { get; set; }

        public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, BaseResponseDto<object>>
        {
            private readonly IServiceUnitOfWork _serviceUnitOfWork;

            public CreateQuizCommandHandler(IServiceUnitOfWork serviceUnitOfWork)
            {
                this._serviceUnitOfWork = serviceUnitOfWork;
            }

            public async Task<BaseResponseDto<object>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
            {
                int quizId = await this._serviceUnitOfWork.QuizService.CreateQuizAsync(request.QuizRequestDto.Title, cancellationToken);

                List<QuestionDto> questionDtos = new List<QuestionDto>();
                foreach (QuestionRequestDto questionRequestDto in request.QuizRequestDto.QuestionRequestDtos)
                {
                    List<ResultOptionDto> resultOptionDtos = questionRequestDto.ResultOptionRequestDtos.Select(x => new ResultOptionDto()
                    {
                        Title = x.Title,
                        IsCorrect = x.IsCorrect
                    }).ToList();

                    questionDtos.Add(new QuestionDto()
                    {
                        QuizId = quizId,
                        Title = questionRequestDto.Title,
                        ResultOptions = resultOptionDtos
                    });
                }

                await this._serviceUnitOfWork.QuizService.AddQuestionsAsync(quizId, questionDtos, cancellationToken);

                return new BaseResponseDto<object>()
                {
                    IsSuccess = true,
                    Message = ResponseMessageEnum.Successful.ToString(),
                    Value = null
                };
            }
        }
    }
}
