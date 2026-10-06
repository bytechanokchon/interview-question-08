using Application.Dtos.Responses;
using Application.Dtos.Services;
using Application.Dtos.Shareds;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Handlers.Quizs.Queries
{
    public class GetQuizDetailQuery : IRequest<BaseResponseDto<List<QuestionResponseDto>>>
    {
        public GetQuizDetailQuery(int quizId)
        {
            this.QuizId = quizId;
        }

        public int QuizId { get; set; }

        public class GetQuizDetailQueryHandler : IRequestHandler<GetQuizDetailQuery, BaseResponseDto<List<QuestionResponseDto>>>
        {
            private readonly IServiceUnitOfWork _serviceUnitOfWork;

            public GetQuizDetailQueryHandler(IServiceUnitOfWork serviceUnitOfWork)
            {
                this._serviceUnitOfWork = serviceUnitOfWork;
            }

            public async Task<BaseResponseDto<List<QuestionResponseDto>>> Handle(GetQuizDetailQuery request, CancellationToken cancellationToken)
            {
                List<QuestionDto> questionDtos = await this._serviceUnitOfWork.QuizService.GetQuestionsByQuizIdAsync(request.QuizId);

                List<QuestionResponseDto> questionResponseDtos = new List<QuestionResponseDto>();
                foreach (QuestionDto questionDto in questionDtos)
                {
                    List<ResultOptionResponseDto> resultOptionResponseDtos = questionDto.ResultOptions.Select(x => new ResultOptionResponseDto()
                    {
                        Id = Convert.ToInt32(x.Id),
                        Title = x.Title
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

                return new BaseResponseDto<List<QuestionResponseDto>>()
                {
                    IsSuccess = true,
                    Message = ResponseMessageEnum.Successful.ToString(),
                    Value = questionResponseDtos
                };
            }
        }
    }
}
