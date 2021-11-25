using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Minimal.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var logger = Logging.CreateLogger();
builder.Services.AddSingleton(logger);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();