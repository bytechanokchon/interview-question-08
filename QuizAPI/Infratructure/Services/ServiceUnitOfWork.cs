using Application.Interfaces;

namespace Infrastructure.Services
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        public ServiceUnitOfWork(IAppDbContext context)
        {
            this.QuizService = new QuizService(context);    
        }
        
        public IQuizService QuizService { get; private set; }
    }
}
