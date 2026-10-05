namespace Application.Dtos.Requests
{
    public class QuestionRequestDto
    {
        public required string Title { get; set; }
        public required List<ResultOptionRequestDto> ResultOptionRequestDtos { get; set; }
    }
}
