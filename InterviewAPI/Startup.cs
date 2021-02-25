using System.Reflection;
using Autofac;
using InterviewService.Infrastructure.Persistence;
using InterviewService.InterviewAPI.Managers;
using InterviewService.InterviewAPI.Managers.AnswerManager;
using InterviewService.InterviewAPI.Managers.InterviewManager;
using InterviewService.InterviewAPI.Managers.OptionManager;
using InterviewService.InterviewAPI.Services;
using InterviewService.InterviewAPI.Services.AnswerService;
using InterviewService.InterviewAPI.Services.CurrentUserService;
using InterviewService.InterviewAPI.Services.InterviewService;
using InterviewService.InterviewAPI.Services.OptionService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace InterviewService.InterviewAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(builder =>
                builder.UseNpgsql(
                    Configuration.GetConnectionString("Application"),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("InterviewService.Infrastructure")));
            services.AddControllers();
            services.AddAutoMapper(Assembly.Load("InterviewService.InterviewAPI"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "InterviewService", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InterviewService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<Services.InterviewService.InterviewService>().As<IInterviewService>();
            builder.RegisterType<InterviewManager>().As<IInterviewManager>();
            builder.RegisterType<AnswerService>().As<IAnswerService>();
            builder.RegisterType<AnswerManager>().As<IAnswerManager>();
            builder.RegisterType<OptionService>().As<IOptionService>();
            builder.RegisterType<OptionManager>().As<IOptionManager>();
            builder.RegisterType<CurrentUserService>().As<ICurrentUserService>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
        }
    }
}