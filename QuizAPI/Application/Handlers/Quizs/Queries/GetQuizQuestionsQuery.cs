using Application.Dtos.Responses;
using Application.Dtos.Services;
using Application.Dtos.Shareds;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Handlers.Quizs.Queries
{
    public class GetQuizQuestionsQuery : IRequest<BaseResponseDto<QuizDetailResponseDto>>
    {
        public GetQuizQuestionsQuery(int quizId)
        {
            this.QuizId = quizId;
        }

        public int QuizId { get; set; }

        public class GetQuizQuestionsQueryHandler : IRequestHandler<GetQuizQuestionsQuery, BaseResponseDto<QuizDetailResponseDto>>
        {
            private readonly IServiceUnitOfWork _serviceUnitOfWork;

            public GetQuizQuestionsQueryHandler(IServiceUnitOfWork serviceUnitOfWork)
            {
                this._serviceUnitOfWork = serviceUnitOfWork;
            }

            public async Task<BaseResponseDto<QuizDetailResponseDto>> Handle(GetQuizQuestionsQuery request, CancellationToken cancellationToken)
            {
                QuizDto quizDto = await this._serviceUnitOfWork.QuizService.GetQuizDetailByIdAsync(request.QuizId);

                List<QuestionDto> questionDtos = await this._serviceUnitOfWork.QuizService.GetQuestionsByQuizIdAsync(request.QuizId);

                List<QuestionResponseDto> questionResponseDtos = new List<QuestionResponseDto>();
                foreach (QuestionDto questionDto in questionDtos)
                {
                    List<ResultOptionResponseDto> resultOptionResponseDtos = questionDto.ResultOptions.Select(x => new ResultOptionResponseDto()
                    {
                        Id = Convert.ToInt32(x.Id),
                        Title = x.Title,
                        IsCorrect = null
                    })
                    .ToList();

                    QuestionResponseDto questionResponseDto = new QuestionResponseDto()
                    {
                        Id = Convert.ToInt32(questionDto.Id),
                        Title = questionDto.Title,
                        ResultOptions = resultOptionResponseDtos
                    };

                    questionResponseDtos.Add(questionResponseDto);
                }

                QuizDetailResponseDto response = new QuizDetailResponseDto()
                {
                    Id = Convert.ToInt32(quizDto.Id),
                    Title = quizDto.Title,
                    Questions = questionResponseDtos
                };

                return new BaseResponseDto<QuizDetailResponseDto>()
                {
                    IsSuccess = true,
                    Message = ResponseMessageEnum.Successful.ToString(),
                    Value = response
                };
            }
        }
    }
}
