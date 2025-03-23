using AspNetCore.Scalar;
using DotNetEnv;
using FleetControl.API.ExceptionsHandler;
using FleetControl.API.Extensions;
using FleetControl.Application;
using FleetControl.Infrastructure;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.TraversePath().Load();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddBuilderExtensionServices();

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddMemoryCache();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference();
    app.UseScalar(options =>
    {
        options.UseTheme(Theme.Default);
        options.RoutePrefix = "api-docs";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.UseCors(Environment.GetEnvironmentVariable("CORS_POLICY_NAME"));

app.UseExceptionHandler();

app.Run();
