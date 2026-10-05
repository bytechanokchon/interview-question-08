namespace Application.Dtos.Requests
{
    public class QuizRequestDto
    {
        public required string Title { get; set; }
        public required List<QuestionRequestDto> QuestionRequestDtos { get; set; }
    }
}
