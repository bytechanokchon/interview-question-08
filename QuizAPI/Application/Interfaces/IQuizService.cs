using Application.Dtos.Services;

namespace Application.Interfaces
{
    public interface IQuizService
    {
        Task<int> CreateQuizAsync(string title, CancellationToken cancellationToken);
        Task AddQuestionsAsync(int quizId, List<QuestionDto> questions, CancellationToken cancellationToken);
        Task<List<QuizDto>> GetQuizs();
        Task<List<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId);
    }
}
