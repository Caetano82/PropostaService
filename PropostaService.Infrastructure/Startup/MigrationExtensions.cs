using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PropostaService.Infrastructure.Startup
{
    public static class MigrationExtensions
    {
        /// <summary>
        /// Aplica migrations do EF Core com tentativas (útil quando o MySQL no Docker ainda está subindo).
        /// </summary>
        public static async Task ApplyMigrationsAsync<TContext>(
            this IServiceProvider services,
            int maxRetries = 10,
            TimeSpan? delay = null) where TContext : DbContext
        {
            delay ??= TimeSpan.FromSeconds(3);

            using var scope = services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
            var db = scope.ServiceProvider.GetRequiredService<TContext>();

            var attempt = 0;
            while (true)
            {
                try
                {
                    logger.LogInformation("Applying EF Core migrations for {Context}...", typeof(TContext).Name);
                    await db.Database.MigrateAsync();
                    logger.LogInformation("Migrations applied successfully for {Context}.", typeof(TContext).Name);
                    break;
                }
                catch (Exception ex) when (attempt < maxRetries)
                {
                    attempt++;
                    logger.LogWarning(ex,
                        "Failed to apply migrations (attempt {Attempt}/{Max}). Retrying in {Delay}...",
                        attempt, maxRetries, delay);
                    await Task.Delay(delay.Value);
                }
            }
        }
    }
}
