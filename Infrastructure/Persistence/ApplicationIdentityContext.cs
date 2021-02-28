using System.Reflection;
using IdentityServer4.EntityFramework.Options;
using InterviewService.Domain.Entities;
using InterviewService.Infrastructure.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InterviewService.Infrastructure.Persistence
{
    public class ApplicationIdentityContext : ApiAuthorizationDbContext<ApplicationUser>
    {
       
        public ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        
    }
}
