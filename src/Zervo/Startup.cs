using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zervo.Middlewares;
using Zervo.Data.Repositories.Database;
using Microsoft.AspNetCore.Http;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Contracts;
using Zervo.Helpers;
using AutoMapper;
using MediatR;
using Zervo.Domain.Services;
using Zervo.Domain.Services.Contracts;
using System.Reflection;
using Zervo.Extensions;

namespace Zervo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable CORS
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });

            // Add dependency injection
            services.AddTransient<IDataContext, ZervoContext>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IEmployeeService, EmployeeService>();

            // Configure EntityFramework / PostgreSQL
            services
                .AddEntityFrameworkNpgsql()
                // DbContext Injected Automatically
                .AddDbContext<ZervoContext>(options =>
                {
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("Zervo.Data"));
                });

            // Add framework services.
            services.AddMvc();

            // Lower case urls
            services.AddRouting(setupAction =>
            {
                setupAction.LowercaseUrls = true;
            });

            // Add Automapper
            services.AddAutoMapper();

            // Add Mediatr
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<SingleInstanceFactory>(sp => t => sp.GetService(t));
            services.AddTransient<MultiInstanceFactory>(sp => t => sp.GetServices(t));
            services.AddMediatorHandlers(typeof(Startup).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Enabl CORS
            app.UseCors("AllowAll");

            app.UseExceptionHandler(
              builder =>
              {
                  builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
              });

            // Add middleware
            app.UseMiddleware<MyTimeLoggerMiddleware>();

            app.UseMvc();

            if (env.IsStaging())
            {
                // Create DB on startup only on Docker
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<ZervoContext>().Database.Migrate();
                }
            }
        }
    }
}
