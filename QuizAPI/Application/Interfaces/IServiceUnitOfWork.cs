namespace Application.Interfaces
{
    public interface IServiceUnitOfWork
    {
        public IQuizService QuizService { get; }
    }
}
