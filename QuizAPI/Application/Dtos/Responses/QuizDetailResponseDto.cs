namespace Application.Dtos.Responses
{
    public class QuizDetailResponseDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required List<QuestionResponseDto> Questions { get; set; }
    }
}
