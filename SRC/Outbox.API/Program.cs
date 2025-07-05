using Mapster;
using Microsoft.EntityFrameworkCore;
using Outbox.API.Context;
using Outbox.API.Dto;
using Outbox.API.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddServiceDefaults();

builder.Services.AddCors();

builder.Services.AddDbContext<OutboxContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("database"));
});

builder.Services.AddOpenTelemetry()
.WithLogging()
.WithMetrics()
.WithTracing(tracing => tracing.AddSource("Outbox"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
    //defaults uı redirect
    app.MapGet("/", context =>
    {
        context.Response.Redirect("scalar/v1");
        return Task.CompletedTask;
    });
}

DbInitializer.ApplyMigration(app.Services);

app.UseCors(x =>
{
    x.AllowAnyMethod()
     .AllowAnyHeader()
     .AllowAnyOrigin();

});

app.UseHttpsRedirection();

app.MapPost("/orders", async (CreateOrderDto request, OutboxContext context, CancellationToken token) =>
{
    Order order = request.Adapt<Order>();

    await context.Orders.AddAsync(order);

    OrderOutbox orderOutbox = new OrderOutbox()
    {
        CreatedAt = DateTime.Now,
        OrderId = order.Id,
    };
    await context.OrderOutbox.AddAsync(orderOutbox);

    await context.SaveChangesAsync(cancellationToken: token);

    return Results.Ok();
});


app.MapGet("orders", async (OutboxContext context, CancellationToken token) =>
{
    List<Order> orders = await context.Orders.ToListAsync(token);

    return Results.Ok(orders);
});

app.Run();


