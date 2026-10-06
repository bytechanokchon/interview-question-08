using Application.Dtos.Shareds;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Handlers.Quizs.Commands
{
    public class DeleteQuizCommand : IRequest<BaseResponseDto<object>>
    {
        public DeleteQuizCommand(int quizId)
        {
            QuizId = quizId;
        }

        public int QuizId { get; }

        public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, BaseResponseDto<object>>
        {
            private readonly IServiceUnitOfWork _serviceUnitOfWork;

            public DeleteQuizCommandHandler(IServiceUnitOfWork serviceUnitOfWork)
            {
                this._serviceUnitOfWork = serviceUnitOfWork;
            }

            public async Task<BaseResponseDto<object>> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
            {
                await this._serviceUnitOfWork.QuizService.DeleteQuizAsync(request.QuizId, cancellationToken);

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
