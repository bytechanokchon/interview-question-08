namespace Application.Dtos.Responses
{
    public class QuestionResponseDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required List<ResultOptionResponseDto> ResultOptions { get; set; }
    }
}
