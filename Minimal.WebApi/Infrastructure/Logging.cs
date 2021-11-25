using Serilog;

namespace Minimal.WebApi.Infrastructure;

public static class Logging
{
    public static ILogger CreateLogger() =>
        new LoggerConfiguration().WriteTo.Console()
                                 .CreateLogger();
}