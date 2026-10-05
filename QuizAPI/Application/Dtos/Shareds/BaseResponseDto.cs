namespace Application.Dtos.Shareds
{
    public class BaseResponseDto<T>
    {
        public required bool IsSuccess { get; set; }
        public required string Message { get; set; }
        public T? Value { get; set; }
    }
}
