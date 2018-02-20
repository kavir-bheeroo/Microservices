using System;
using System.Collections.Generic;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Services.Revenue.API.Infrastructure
{
    public static class MediatorSetup
    {
        public static void Run(IServiceCollection services)
        {
            services.AddScoped<SingleInstanceFactory>(p => p.GetRequiredService);
            services.AddScoped<MultiInstanceFactory>(p => p.GetRequiredServices);

            //Pipeline
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            // services.AddScoped(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>));
            // services.AddScoped(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));
            // services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(GenericRequestPostProcessor<,>));

            //This causes a type load exception. https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection/issues/12
            //services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(ConstrainedRequestPostProcessor<,>));
            //services.AddScoped(typeof(INotificationHandler<>), typeof(ConstrainedPingedHandler<>));

            // Use Scrutor to scan and register all
            // classes as their implemented interfaces.
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMediator))
                .AddClasses()
                .AsImplementedInterfaces());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IRequestHandler<,>))
                .AddClasses()
            );
        }

        private static IEnumerable<object> GetRequiredServices(this IServiceProvider provider, Type serviceType)
        {
            return (IEnumerable<object>) provider.GetRequiredService(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }
    }
}