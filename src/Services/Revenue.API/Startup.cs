using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics.AspNetCore.Health.Endpoints;
using App.Metrics.Health;
using App.Metrics.Health.Builder;
using FluentValidation;
using MediatR;
using Microservices.BuildingBlocks.HealthChecks;
using Microservices.Services.Revenue.API.Application.Behaviors;
using Microservices.Services.Revenue.API.Application.Commands;
using Microservices.Services.Revenue.API.Application.Queries;
using Microservices.Services.Revenue.API.Application.Validations;
using Microservices.Services.Revenue.Domain.AggregatesModel.DailyRevenueAggregate;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microservices.Services.Revenue.Infrastructure;
using Microservices.Services.Revenue.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            var revenueDbConnectionString = Configuration.GetConnectionString("RevenueDb");

            // All MediatR dependencies like CommandHandlers and DomainEventHandlers are registered automatically with the following line.
            services.AddMediatR();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            // Validators still need to be registered manually.
            services.AddScoped<IValidator<CreateTripCommand>, CreateTripCommandValidator>();

            // EF Core dependencies
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<RevenueContext>(options =>
                {
                    options.UseSqlServer(revenueDbConnectionString,
                        sqlOptions => 
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                });           

            services.AddScoped<IDailyRevenueRepository, DailyRevenueRepository>();
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<ITripQueries>(sp => new TripQueries(revenueDbConnectionString));

            // Add health checks
            var healthBuilder = new HealthBuilder()
                .HealthChecks.AddSqlCheck("RevenueDb", revenueDbConnectionString);  
            services.AddHealth(healthBuilder.Builder);
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<HealthEndpointsHostingOptions>, HealthCheckOptions>());

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthAllEndpoints();
            app.UseMvc();
        }
    }
}
