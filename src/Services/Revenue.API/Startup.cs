using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microservices.Services.Revenue.API.Application.Behaviors;
using Microservices.Services.Revenue.API.Application.Commands;
using Microservices.Services.Revenue.API.Application.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Revenue.API
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
            services.AddMediatR();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            // services.Scan(scan => scan
            //     .FromAssemblyOf<CreateTripCommandValidator>()
            //     .AddClasses((f) => f.Where(t => t.IsSubclassOf(typeof(IValidator<>)))));

            services.AddScoped(typeof(IValidator<CreateTripCommand>), typeof(CreateTripCommandValidator));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
