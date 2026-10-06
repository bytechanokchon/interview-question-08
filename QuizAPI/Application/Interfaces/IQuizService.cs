using Application.Dtos.Services;

namespace Application.Interfaces
{
    public interface IQuizService
    {
        Task<int> CreateQuizAsync(string title, CancellationToken cancellationToken);
        Task AddQuestionsAsync(int quizId, List<QuestionDto> questions, CancellationToken cancellationToken);
        Task<List<QuizDto>> GetQuizs();
        Task<List<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId);
        Task UpdateQuizAsync(int quizId, string title, CancellationToken cancellationToken);
        Task<int> AddQuestionAsync(int quizId, string title, CancellationToken cancellationToken);
        Task UpdateQuestionAsync(int questionId, string title, CancellationToken cancellationToken);
        Task DeleteQuestionAsync(int questionId, CancellationToken cancellationToken);
        Task AddResultOptionAsync(int questionId, string title, bool isCorrect, CancellationToken cancellationToken);
        Task UpdateResultOptionAsync(int resultOptionId, string title, bool isCorrect, CancellationToken cancellationToken);
        Task RemoveResultOptionAsync(int resultOptionId, CancellationToken cancellationToken);
    }
}
