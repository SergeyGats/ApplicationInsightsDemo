using ApplicationInsightsDemo.DataAccess.DatabaseContexts.Interfaces;
using ApplicationInsightsDemo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationInsightsDemo.DataAccess.DatabaseContexts.Implementations
{
    public class ApplicationInsightsDemoDbContext : DbContext, IApplicationInsightsDemoDbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public ApplicationInsightsDemoDbContext(DbContextOptions<ApplicationInsightsDemoDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationInsightsDemoDataAccessAssemblyReference).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}