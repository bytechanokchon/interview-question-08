namespace Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public required string Title { get; set; }
        public required DateTime CreatedAt { get; set; }

        public required Quiz Quiz { get; set; }
        public required List<ResultOption> ResultOptions { get; set; }
    }
}
