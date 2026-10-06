namespace Application.Dtos.Services
{
    public class QuizDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
