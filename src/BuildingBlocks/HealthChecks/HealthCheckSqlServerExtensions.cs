using System;
using System.Data;
using System.Data.SqlClient;
using App.Metrics.Health;

namespace Microservices.BuildingBlocks.HealthChecks
{
    public static class HealthCheckBuilderSqlServerExtensions
    {
        public static IHealthCheckBuilder AddSqlCheck(this IHealthCheckBuilder builder, string name, string connectionString)
        {
            builder.AddCheck($"SqlCheck({name})", async () =>
            {
                try
                {
                    // TODO: There is probably a much better way to do this.
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandType = CommandType.Text;
                            command.CommandText = "SELECT 1";
                            var result = (int)await command.ExecuteScalarAsync().ConfigureAwait(false);

                            if (result == 1)
                            {
                                return HealthCheckResult.Healthy($"SqlCheck({name}): Healthy");
                            }

                            return HealthCheckResult.Unhealthy($"SqlCheck({name}): Unhealthy");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"SqlCheck({name}): Exception during check: {ex.GetType().FullName}");
                }
            });

            return builder;
        }
    }
}