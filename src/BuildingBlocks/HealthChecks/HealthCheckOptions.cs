using App.Metrics.AspNetCore.Health.Endpoints;
using Microsoft.Extensions.Options;

namespace Microservices.BuildingBlocks.HealthChecks
{
    public class HealthCheckOptions : IConfigureOptions<HealthEndpointsHostingOptions>
    {
        public void Configure(HealthEndpointsHostingOptions options)
        { 
            options.HealthEndpoint = "/_system/health";
            options.PingEndpoint = "/_system/ping";
        }
    }
}