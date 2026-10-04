namespace Domain.Entities
{
    public class ResultOption
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public required string Title { get; set; }
        public required bool IsCorrect { get; set; }
        public required DateTime CreatedAt { get; set; }

        public required Question Question { get; set; }
    }
}
