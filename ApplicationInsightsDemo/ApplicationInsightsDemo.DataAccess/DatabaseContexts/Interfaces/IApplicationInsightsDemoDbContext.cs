using ApplicationInsightsDemo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationInsightsDemo.DataAccess.DatabaseContexts.Interfaces
{
    public interface IApplicationInsightsDemoDbContext
    {
        DbSet<UserEntity> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}