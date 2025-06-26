using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddServiceDefaults();

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

app.UseHttpsRedirection();


app.Run();


