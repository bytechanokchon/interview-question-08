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
                    IsCorrect = Convert.ToBoolean(x.IsCorrect),
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

        public async Task<List<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId)
        {
            return await this._context.Questions
                .Include(x => x.ResultOptions)
                .Where(x => x.QuizId == quizId)
                .Select(x => new QuestionDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ResultOptions = x.ResultOptions.Select(y => new ResultOptionDto()
                    {
                        Id = y.Id,
                        Title = y.Title
                    })
                    .ToList()
                })
                .ToListAsync();
        }

        public async Task UpdateQuizAsync(int quizId, string title, CancellationToken cancellationToken)
        {
            Quiz? entity = await this._context.Quizs.Where(x => x.Id == quizId).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Title = title;

                this._context.Quizs.Update(entity);
                await this._context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<int> AddQuestionAsync(int quizId, string title, CancellationToken cancellationToken)
        {
            Question entity = new Question()
            {
                QuizId = quizId,
                Title = title,
                CreatedAt = DateTime.Now,
            };

            await this._context.Questions.AddAsync(entity);
            await this._context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task UpdateQuestionAsync(int questionId, string title, CancellationToken cancellationToken)
        {
            Question? entity = await this._context.Questions.Where(x => x.Id == questionId).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Title = title;

                this._context.Questions.Update(entity);
                await this._context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteQuestionAsync(int questionId, CancellationToken cancellationToken)
        {
            Question? entity = await this._context.Questions.Where(x => x.Id == questionId).FirstOrDefaultAsync();

            if (entity != null)
            {
                this._context.Questions.Remove(entity);
                await this._context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task AddResultOptionAsync(int questionId, string title, bool isCorrect, CancellationToken cancellationToken)
        {
            ResultOption entity = new ResultOption()
            {
                QuestionId = questionId,
                Title = title,
                IsCorrect = isCorrect,
                CreatedAt = DateTime.Now
            };

            await this._context.ResultOptions.AddAsync(entity);
            await this._context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateResultOptionAsync(int resultOptionId, string title, bool isCorrect, CancellationToken cancellationToken)
        {
            ResultOption? entity = await this._context.ResultOptions.Where(x => x.Id == resultOptionId).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Title = title;
                entity.IsCorrect = isCorrect;

                this._context.ResultOptions.Update(entity);
                await this._context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task RemoveResultOptionAsync(int resultOptionId, CancellationToken cancellationToken)
        {
            ResultOption? entity = await this._context.ResultOptions.Where(x => x.Id == resultOptionId).FirstOrDefaultAsync();

            if (entity != null)
            {
                this._context.ResultOptions.Remove(entity);
                await this._context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteQuizAsync(int quizId, CancellationToken cancellationToken)
        {
            Quiz? entity = await this._context.Quizs
                .Include(x => x.Questions)
                    .ThenInclude(x => x.ResultOptions)
                .Where(x => x.Id == quizId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                this._context.Quizs.Remove(entity);
                await this._context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<QuestionResultDto>> GetQuizResultsAsync(int quizId)
        {
            var quiz = await this._context.Quizs
                .Include(x => x.Questions)
                    .ThenInclude(x => x.ResultOptions)
                .Where(x => x.Id == quizId)
                .FirstOrDefaultAsync();

            List<QuestionResultDto> result = new List<QuestionResultDto>();
            if (quiz.Questions != null)
            {
                foreach (var question in quiz.Questions)
                {
                    var correctResultOption = question.ResultOptions.Where(x => x.IsCorrect == true).FirstOrDefault();

                    if (correctResultOption != null)
                    {
                        result.Add(new QuestionResultDto()
                        {
                            QuestionId = question.Id,
                            ResultOptionId = correctResultOption.Id
                        });
                    }
                }
            }

            return result;
        }
    }
}
