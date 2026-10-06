namespace Application.Dtos.Requests
{
    public class QuizUpdateRequestDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required List<QuestionUpdateRequestDto> Questions { get; set; }
    }
}
