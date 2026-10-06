using Application.Dtos.Responses;
using Application.Dtos.Services;
using Application.Dtos.Shareds;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Handlers.Quizs.Queries
{
    public class GetQuizQuery : IRequest<BaseResponseDto<List<QuizResponseDto>>>
    {
        public class GetQuizQueryHandler : IRequestHandler<GetQuizQuery, BaseResponseDto<List<QuizResponseDto>>>
        {
            private readonly IServiceUnitOfWork _serviceUnitOfWork;

            public GetQuizQueryHandler(IServiceUnitOfWork serviceUnitOfWork)
            {
                this._serviceUnitOfWork = serviceUnitOfWork;
            }

            public async Task<BaseResponseDto<List<QuizResponseDto>>> Handle(GetQuizQuery request, CancellationToken cancellationToken)
            {
                List<QuizDto> quizDtos = await this._serviceUnitOfWork.QuizService.GetQuizs();

                List<QuizResponseDto> responses = quizDtos.Select(x => new QuizResponseDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreatedAt = x.CreatedAt
                })
                .ToList();

                return new BaseResponseDto<List<QuizResponseDto>>()
                {
                    IsSuccess = true,
                    Message = ResponseMessageEnum.Successful.ToString(),
                    Value = responses
                };
            }
        }
    }
}
