using System.Linq;
using System.Reflection;
using Autofac;
using InterviewService.Infrastructure.Identity;
using InterviewService.Infrastructure.Persistence;
using InterviewService.InterviewAPI.Managers;
using InterviewService.InterviewAPI.Managers.AnswerManager;
using InterviewService.InterviewAPI.Managers.InterviewManager;
using InterviewService.InterviewAPI.Managers.OptionManager;
using InterviewService.InterviewAPI.Services;
using InterviewService.InterviewAPI.Services.AnswerService;
using InterviewService.InterviewAPI.Services.CurrentUserService;
using InterviewService.InterviewAPI.Services.IdentityService;
using InterviewService.InterviewAPI.Services.InterviewService;
using InterviewService.InterviewAPI.Services.OptionService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            
            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationIdentityContext>();
            
            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddRazorPages();
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });
            
            services.AddDbContext<ApplicationDbContext>(builder =>
                builder.UseNpgsql(
                    Configuration.GetConnectionString("Application"),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("InterviewService.Infrastructure")));

            services.AddDbContext<ApplicationIdentityContext>(builder =>
                builder.UseNpgsql(
                    Configuration.GetConnectionString("Identity"),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("InterviewService.Infrastructure")));
            
            services.AddControllers();
            services.AddAutoMapper(Assembly.Load("InterviewService.InterviewAPI"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "InterviewService", Version = "v1"});
            });
            
            /*services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });*/
            
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

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); endpoints.MapRazorPages(); });
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
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
            builder.RegisterType<IdentityService>().As<IIdentityService>();
        }
    }
}