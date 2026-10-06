using Application.Dtos.Requests;
using Application.Dtos.Shareds;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Handlers.Quizs.Commands
{
    public class UpdateQuizCommand : IRequest<BaseResponseDto<object>>
    {
        public UpdateQuizCommand(QuizUpdateRequestDto quizUpdateRequestDto)
        {
            QuizUpdateRequestDto = quizUpdateRequestDto;
        }

        public QuizUpdateRequestDto QuizUpdateRequestDto { get; }

        public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, BaseResponseDto<object>>
        {
            private readonly IServiceUnitOfWork _serviceUnitOfWork;

            public UpdateQuizCommandHandler(IServiceUnitOfWork serviceUnitOfWork)
            {
                this._serviceUnitOfWork = serviceUnitOfWork;
            }

            public async Task<BaseResponseDto<object>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
            {
                await this._serviceUnitOfWork
                    .QuizService
                    .UpdateQuizAsync(request.QuizUpdateRequestDto.Id, request.QuizUpdateRequestDto.Title, cancellationToken);

                foreach (QuestionUpdateRequestDto questionRequestDto in request.QuizUpdateRequestDto.Questions)
                {
                    int? questionId = questionRequestDto.Id;

                    if (questionRequestDto.RowState == (int)RowStateEnum.CREATE)
                    {
                        questionId = await this._serviceUnitOfWork.QuizService.AddQuestionAsync(request.QuizUpdateRequestDto.Id, questionRequestDto.Title, cancellationToken);
                    }
                    else if (questionRequestDto.RowState == (int)RowStateEnum.UPDATE)
                    {
                        await this._serviceUnitOfWork.QuizService.UpdateQuestionAsync(Convert.ToInt32(questionRequestDto.Id), questionRequestDto.Title, cancellationToken);
                    }
                    else if (questionRequestDto.RowState == (int)RowStateEnum.DELETE)
                    {
                        await this._serviceUnitOfWork.QuizService.DeleteQuestionAsync(Convert.ToInt32(questionRequestDto.Id), cancellationToken);
                    }

                    foreach (ResultOptionUpdateRequestDto resultOptionUpdateRequestDto in questionRequestDto.ResultOptions)
                    {
                        if (resultOptionUpdateRequestDto.RowState == (int)RowStateEnum.CREATE)
                        {
                            await this._serviceUnitOfWork
                                .QuizService
                                .AddResultOptionAsync(Convert.ToInt32(questionId), resultOptionUpdateRequestDto.Title, resultOptionUpdateRequestDto.IsCorrect, cancellationToken);
                        }
                        else if (resultOptionUpdateRequestDto.RowState == (int)RowStateEnum.UPDATE)
                        {
                            await this._serviceUnitOfWork
                                .QuizService
                                .UpdateResultOptionAsync(Convert.ToInt32(resultOptionUpdateRequestDto.Id), resultOptionUpdateRequestDto.Title, resultOptionUpdateRequestDto.IsCorrect, cancellationToken);
                        }
                        else if (resultOptionUpdateRequestDto.RowState == (int)RowStateEnum.DELETE)
                        {
                            await this._serviceUnitOfWork
                                .QuizService
                                .RemoveResultOptionAsync(Convert.ToInt32(resultOptionUpdateRequestDto.Id), cancellationToken);
                        }
                    }
                }

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
