using Application.Dtos.Services;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class QuizService : IQuizService
    {
        private readonly IAppDbContext _context;

        public QuizService(IAppDbContext context)
        {
            this._context = context;
        }

        public async Task<int> CreateQuizAsync(string title, CancellationToken cancellationToken)
        {
            Quiz entity = new Quiz()
            {
                Title = title,
                CreatedAt = DateTime.Now,
            };

            await this._context.Quizs.AddAsync(entity);

            await this._context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task AddQuestionsAsync(int quizId, List<QuestionDto> questions, CancellationToken cancellationToken)
        {
            List<Question> questionEntities = new List<Question>();
            DateTime createdAt = DateTime.Now;

            foreach (QuestionDto question in questions)
            {
                List<ResultOption> resultOptionEntities = question.ResultOptions.Select(x => new ResultOption()
                {
                    IsCorrect = x.IsCorrect,
                    Title = x.Title,
                    CreatedAt = createdAt
                })
                .ToList();

                questionEntities.Add(new Question()
                {
                    QuizId = quizId,
                    Title = question.Title,
                    CreatedAt = createdAt,
                    ResultOptions = resultOptionEntities,
                });
            }

            await this._context.Questions.AddRangeAsync(questionEntities);

            await this._context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<QuizDto>> GetQuizs()
        {
            return await this._context.Quizs.Select(x => new QuizDto()
            {
                Id = x.Id,
                Title = x.Title,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
        }
    }
}
