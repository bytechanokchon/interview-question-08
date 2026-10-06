namespace Application.Dtos.Requests
{
    public class CheckScoreRequestDto
    {
        public required int QuizId { get; set; }
        public required List<CheckScoreQuestionRequestDto> Questions  { get; set; }
    }
}
