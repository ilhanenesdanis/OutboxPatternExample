using System;
using Microsoft.EntityFrameworkCore;

namespace Outbox.API.Context;

public static class DbInitializer
{
    public static void ApplyMigration(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DbInitializer");

        try
        {
            logger.LogInformation("Migrate Started");

            var context = scope.ServiceProvider.GetRequiredService<OutboxContext>();
            context.Database.Migrate();

            logger.LogInformation("Database migration successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DbInitializer Migrate exception");

            throw;
        }
    }
}
