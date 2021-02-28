using System.Reflection;
using InterviewService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewService.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Interview> Interviews { get; set; }
        
        public DbSet<Option> Options { get; set; }
        
        public DbSet<Answer> Answers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        
    }
}