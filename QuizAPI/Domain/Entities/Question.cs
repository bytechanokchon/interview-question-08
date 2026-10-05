namespace Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public required string Title { get; set; }
        public required DateTime CreatedAt { get; set; }

        public Quiz Quiz { get; set; } = null!;
        public List<ResultOption> ResultOptions { get; set; } = null!;
    }
}
