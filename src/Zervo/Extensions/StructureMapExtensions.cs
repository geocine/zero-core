using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using Zervo.Validators;

namespace Zervo.Extensions
{
    public static class StructureMapExtensions
    {
        // StructureMap vs Microsoft
        // services.AddTransient<T>(sp => t => sp.GetService(t));
        // config.For<T>().Use<T>(ctx => t => ctx.GetInstance(t)).Transient();

        // services.AddScoped<IT, T>();
        // config.For<IT>().Use<T>().ContainerScoped();

        public static void AddMediatorHandlers(this ConfigurationExpression config)
        {
            config.Scan(c =>
             {
                 c.Assembly(typeof(Startup).GetTypeInfo().Assembly);
                 c.WithDefaultConventions();
                 c.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                 c.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
             });
        }

        public static void AddFluentValidators(this ConfigurationExpression config)
        {
            config.Scan(c =>
            {
                c.Assembly(typeof(Startup).GetTypeInfo().Assembly);
                c.WithDefaultConventions();
                c.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
            });
        }

        public static void DecorateMediatorWithFluentValidators(this ConfigurationExpression config)
        {
            // Learn about Decorators
            // https://lostechies.com/jimmybogard/2014/09/09/tackling-cross-cutting-concerns-with-a-mediator-pipeline/
            var asyncHandlerType = config.For(typeof(IAsyncRequestHandler<,>));
            asyncHandlerType.DecorateAllWith(typeof(AsyncValidatorHandler<,>));
        }


    }
}
