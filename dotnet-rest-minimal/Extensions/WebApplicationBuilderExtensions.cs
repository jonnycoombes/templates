using Serilog;

namespace JCS.Neon.Rest.Api.Extensions;

/// <summary>
/// Extensions for <see cref="WebApplicationBuilder"/> containing global utility functions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Configures logging based on Serilog, rather than the default
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/></param>
    /// <returns>The <see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        var logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(builder.Configuration)
            .CreateLogger();
        builder.Logging.AddSerilog(logger);
        return builder;
    }
}