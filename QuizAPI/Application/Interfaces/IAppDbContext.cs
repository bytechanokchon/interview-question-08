using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        public DbSet<Quiz> Quizs { get; }
        public DbSet<Question> Questions { get; }
        public DbSet<ResultOption> ResultOptions { get; }
    }
}
