using System;
using Microsoft.EntityFrameworkCore;
using Outbox.API.Context;

namespace Outbox.API.BackgroundServices;

public sealed class OrderBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;

    public OrderBackgroundService(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scoped = _services.CreateScope())
        {
            var dbContext = scoped.ServiceProvider.GetRequiredService<OutboxContext>();

            var orderOutboxes = await dbContext.OrderOutbox.Where(x => !x.IsComplated).OrderBy(x => x.CreatedAt).ToListAsync(stoppingToken);

            foreach (var item in orderOutboxes)
            {
                System.Console.WriteLine(item.OrderId);

                var now = DateTime.Now;
                item.ComplatedDate = now;
                item.IsComplated = true;
                item.LastAttempt = now;


            }
            await dbContext.SaveChangesAsync();
        }
    }
}
