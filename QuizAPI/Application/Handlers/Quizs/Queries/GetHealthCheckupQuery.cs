using Application.Dtos.Responses;
using Domain.Enums;
using MediatR;

namespace Application.Handlers.Quizs.Queries
{
    public class GetHealthCheckupQuery : IRequest<BaseResponseDto<object>>
    {
        public class GetHealthCheckupQueryHandler : IRequestHandler<GetHealthCheckupQuery, BaseResponseDto<object>>
        {
            public async Task<BaseResponseDto<object>> Handle(GetHealthCheckupQuery request, CancellationToken cancellationToken)
            {
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
