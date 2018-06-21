﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics.AspNetCore.Health.Endpoints;
using App.Metrics.Health;
using App.Metrics.Health.Builder;
using Microservices.BuildingBlocks.EventBus;
using Microservices.BuildingBlocks.EventBus.Abstractions;
using Microservices.BuildingBlocks.EventBusRabbitMQ;
using Microservices.BuildingBlocks.HealthChecks;
using Microservices.Services.Roster.API.IntegrationEvents.EventHandling;
using Microservices.Services.Roster.API.IntegrationEvents.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Roster.API
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
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration.GetValue<string>("Endpoints:RabbitMq")
                };

                if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                {
                    factory.UserName = Configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                {
                    factory.Password = Configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, retryCount);
            });

            RegisterEventBus(services);

            var identityServerEndpoint = Configuration.GetValue<string>("Endpoints:IdentityServer");

            // Add health checks
            var healthBuilder = new HealthBuilder()
                .HealthChecks.AddHttpGetCheck("IdentityServer", new Uri(new Uri(identityServerEndpoint), "/_system/health"), 3, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));  
            services.AddHealth(healthBuilder.Build());
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

            ConfigureEventBus(app);

            app.UseHealthAllEndpoints();
            app.UseMvc();
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            var subscriptionClientName = Configuration.GetValue<string>("SubscriptionClientName");

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<BusAddedIntegrationEvent, BusAddedIntegrationEventHandler>();
        }
    }
}
