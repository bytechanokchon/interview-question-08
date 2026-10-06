using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Dtos.Services;
using Application.Dtos.Shareds;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Handlers.Quizs.Queries
{
    public class CheckResultQuery : IRequest<BaseResponseDto<ScoreResultResponseDto>>
    {
        public CheckResultQuery(CheckScoreRequestDto checkScoreRequestDto)
        {
            CheckScoreRequestDto = checkScoreRequestDto;
        }

        public CheckScoreRequestDto CheckScoreRequestDto { get; }

        public class CheckResultQueryHandler : IRequestHandler<CheckResultQuery, BaseResponseDto<ScoreResultResponseDto>>
        {
            private readonly IServiceUnitOfWork _serviceUnitOfWork;

            public CheckResultQueryHandler(IServiceUnitOfWork serviceUnitOfWork)
            {
                this._serviceUnitOfWork = serviceUnitOfWork;
            }

            public async Task<BaseResponseDto<ScoreResultResponseDto>> Handle(CheckResultQuery request, CancellationToken cancellationToken)
            {
                List<QuestionResultDto> questionResultDtos = await this._serviceUnitOfWork.QuizService.GetQuizResultsAsync(request.CheckScoreRequestDto.QuizId);

                int fullScore = questionResultDtos.Count;
                int score = 0;

                foreach (CheckScoreQuestionRequestDto checkScoreQuestionRequestDto in request.CheckScoreRequestDto.Questions)
                {
                    bool isCorrect = questionResultDtos
                        .Where(x => x.QuestionId == checkScoreQuestionRequestDto.QuestionId && x.ResultOptionId == checkScoreQuestionRequestDto.ResultOptionId)
                        .Any();

                    if (isCorrect) score++;
                }

                return new BaseResponseDto<ScoreResultResponseDto>()
                {
                    IsSuccess = true,
                    Message = ResponseMessageEnum.Successful.ToString(),
                    Value = new ScoreResultResponseDto()
                    {
                        FullScore = fullScore,
                        ScoreObtained = score
                    }
                };
            }
        }
    }
}
