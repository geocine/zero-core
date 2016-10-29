using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
using System;
using System.IO;
using System.Net;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Serilog;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Contracts;
using Zervo.Data.Repositories.Database;
using Zervo.Domain.Services;
using Zervo.Domain.Services.Contracts;
using Zervo.Extensions;
using Zervo.GraphQL;
using Zervo.Middlewares;

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

            // Rolling file Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "log-{Date}.txt"))
                .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ITokenService, TokenService>();
           

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

            services.AddZervoIdentity();

            return ConfigureIoC(services);
        }

        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            // Add Mediatr
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<SingleInstanceFactory>(sp => t => sp.GetService(t));
            services.AddTransient<MultiInstanceFactory>(sp => t => sp.GetServices(t));

            // This is using StructureMap.Microsoft.DependencyInjection to work with
            // Microsoft.Extensions.DependencyInjection So you could use both

            var container = new Container();
            container.Configure(config =>
            {
                config.AddMediatorHandlers();
                config.AddFluentValidators();
                // validations will be called for every mediator request
                config.DecorateMediator();

                //GraphQL
                config.For<IDocumentExecuter>().Use<DocumentExecuter>();
                config.For<IDocumentWriter>().Use(x => new DocumentWriter(true));
                config.For<CustomerType>().Use<CustomerType>();
                config.For<ZervoSchema>().Use(x => new ZervoSchema(type => (GraphType) container.GetInstance(type)));

            });
            container.Populate(services);
            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // Add Serilog - Logs to Rolling File
            loggerFactory.AddSerilog();

            // Enabl CORS
            app.UseCors("AllowAll");

            app.UseExceptionHandler(
              builder =>
              {
                  builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            //context.Response.AddApplicationError(error.Error.Message);
                            //serviceProvider.GetService<ILogger<Startup>>().LogError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
              });

            // Add middleware
            app.UseMiddleware<MyTimeLoggerMiddleware>();

            // Identity tuff
            app.UseZervoIdentity();

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