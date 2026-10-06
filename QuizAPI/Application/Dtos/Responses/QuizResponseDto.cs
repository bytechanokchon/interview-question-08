namespace Application.Dtos.Responses
{
    public class QuizResponseDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
